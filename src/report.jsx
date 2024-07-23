
import React, { useEffect, useRef, useState } from 'react';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { getAllGifts } from './axios/giftAxios';
import { Toast } from 'primereact/toast';
import { GetSaleByGift } from './axios/saleAxios';


export default function Report(props) {
    const [products, setProducts] = useState([]);
    // const [visible, setVisible] = useState(false);
    const [expandedRows, setExpandedRows] = useState([]);
    const toast = useRef(null);

    useEffect(() => {
        async function fetchData() {
            await getAllGifts().then((data) => setProducts(data));
        }
        fetchData();
    }, []);

    useEffect(() => {
        async function fetchData() {
            await products.forEach(async element => {
                element.Gift= await GetSaleByGift(element.id)
            });
            console.log("88888888888888888",products);
        }
        fetchData();
    }, [products]);
    
    const allowExpansion = (rowData) => {
        return true;
    };
   

    const rowExpansionTemplate = (data) => {
        return (
            <div className="p-3">
                <h5>Sales of  {data.name}</h5>
                <DataTable value={data.Gift}
                dataKey="id" tableStyle={{ minWidth: '60rem' }}>
                    <Column field="id" header="Id sale" sortable></Column>
                    <Column field="customer.name" header="Customer name" sortable></Column>
                    <Column field="customer.phone" header="Customer phone" sortable></Column>
                    <Column field="customer.email" header="Customer email" sortable></Column>
                    <Column field="customer.address" header="Customer address" sortable></Column>
                    <Column field="count" header="Amount of tickets" sortable></Column>
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
                dataKey="id" tableStyle={{ minWidth: '60rem' }}>
                <Column expander={allowExpansion} style={{ width: '5rem' }} />
                <Column field="name" header="Name" sortable  style={{ width: '20%' }}></Column>
                <Column field="description" header="Description" sortable style={{ width: '20%' }}></Column>
                <Column field="price" header="Price" sortable></Column>
            </DataTable>
        </div>
    );
}




