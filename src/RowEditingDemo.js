
import React, { useEffect, useRef, useState } from 'react';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { InputText } from 'primereact/inputtext';
import { getAllDonors, updateDonor } from './axios/donorAxios'
import { Button } from 'primereact/button';
import { Dialog } from 'primereact/dialog';
import { classNames } from 'primereact/utils';
import { getGiftsByDonor } from './axios/giftAxios';
import { Toast } from 'primereact/toast';


export default function ShowDonors(props) {
    const [products, setProducts] = useState([]);
    // const [visible, setVisible] = useState(false);
    const [expandedRows, setExpandedRows] = useState(null);
    const toast = useRef(null);

    useEffect(() => {
        async function fetchData() {
            await getAllDonors().then((data) => setProducts(data));
        }
        fetchData();
    }, []);

    useEffect(() => {
        async function fetchData() {
            await products.forEach(async element => {
                element.Gift= await getGiftsByDonor(element.id)
            });
            console.log(products);
        }
        fetchData();
    }, [products]);

    const onRowEditComplete = async (e) => {
        let _products = [...products];
        let { newData, index } = e;
        _products[index] = newData;
        debugger
        const a = {
            "Id": e.data.id,
            "Name": e.newData.name,
            "Phone": e.newData.phone,
            "Email": e.newData.email,
            "Address": e.newData.address
        }
        await updateDonor(a).then((e) => {
            console.log("updateD", e)
            if (e.status != 200) {
                console.log("errorUpdateDonor");
                debugger
                // props.setErrorUpdate(true)
            }
            setProducts(_products);
        })
    };

    const textEditor = (options) => {
        return <InputText type="text" value={options.value} onChange={(e) => options.editorCallback(e.target.value)} />;
    };

    const allowEdit = (rowData) => {
        return rowData.name !== 'Blue Band';
    };
    const expandAll = () => {
        let _expandedRows = {};

        products.forEach((p) => (_expandedRows[`${p.id}`] = true));

        setExpandedRows(_expandedRows);
    };

    const collapseAll = () => {
        setExpandedRows(null);
    };

    const formatCurrency = (value) => {
        return value.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
    };

    const amountBodyTemplate = (rowData) => {
        return formatCurrency(rowData.amount);
    };

    
    const allowExpansion = (rowData) => {
        return true;
    };

    const rowExpansionTemplate = (data) => {
        return (
            <div className="p-3">
                <h5>Gifts of  {data.name}</h5>
                <DataTable value={data.Gift}>
                    <Column field="id" header="Id" sortable></Column>
                    <Column field="name" header="Mame" sortable></Column>
                    <Column field="description" header="Description" sortable></Column>
                    <Column field="price" header="Price" sortable></Column>
                </DataTable>
            </div>
        );
    };

    return (
        <div className="card">
            <Toast ref={toast} />
            <DataTable value={products} 
                expandedRows={expandedRows}
                editMode="row"
                onRowToggle={(e) => setExpandedRows(e.data)}
                rowExpansionTemplate={rowExpansionTemplate}
                onRowEditComplete={onRowEditComplete}
                dataKey="id" tableStyle={{ minWidth: '60rem' }}>
                <Column expander={allowExpansion} style={{ width: '5rem' }} />
                <Column field="name" header="Name" sortable editor={(options) => textEditor(options)} style={{ width: '20%' }}></Column>
                <Column field="email" header="Email" sortable editor={(options) => textEditor(options)} style={{ width: '20%' }}></Column>
                <Column field="phone" header="Phone" sortable editor={(options) => textEditor(options)} style={{ width: '20%' }}></Column>
                <Column field="address" header="Address" sortable editor={(options) => textEditor(options)} style={{ width: '20%' }}></Column>
                <Column rowEditor={allowEdit} headerStyle={{ width: '10%', minWidth: '8rem' }} bodyStyle={{ textAlign: 'center' }}></Column>
            </DataTable>
        </div>
    );
}




