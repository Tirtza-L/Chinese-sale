
import React, { useState, useEffect } from 'react';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Button } from 'primereact/button';
import { GetSaleByCustomer, changeToStatusTrue, deleteSale } from './axios/saleAxios';
import { jwtDecode } from 'jwt-decode';
import { getAllGifts } from './axios/giftAxios';
import { Toast } from 'primereact/toast';
import { useRef } from 'react';
import { ConfirmDialog, confirmDialog } from 'primereact/confirmdialog';


export default function ShoppingCart() {
    const [products, setProducts] = useState([]);
    const [productsNo, setProductsNo] = useState([]);
    const [gifts, setGifts] = useState([]);
    const [buy, setBuy] = useState(false);
    const [detailError, setDetailError] = useState(null);
    const [detailSuccess, setDetailSuccess] = useState(null);
    const [visible, setVisible] = useState(false);
    const [accept1, setAccept] = useState(false);

    const toast = useRef(null);

    const TwoLists = (data) => {
        if (!Array.isArray(data)) {
            console.error("Data is not an array");
            return;
        }

        data.forEach(element => {
            if (element.status == true)
                setProducts(prevProducts => [...prevProducts, element])
            else
                setProductsNo(prevProductsNo => [...prevProductsNo, element])
        })
    }

    useEffect(() => {
        const a = async () => {
            debugger
            const token = localStorage.getItem("token")
            var decodedToken = jwtDecode(token)
            const id = parseInt(decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"])

            await GetSaleByCustomer(id).then((data) => TwoLists(data));
            debugger
        }
        a()
    }, []);

    useEffect(() => {
        const a = async () => {
            await getAllGifts().then((data) => setGifts(data));
            debugger
        }
        a()
    }, []);


    const showSuccess = () => {
        toast.current.show({ severity: 'success', summary: 'Success', detail: `${detailSuccess}`, life: 3000 });
        setDetailSuccess(null)
    }

    const showError = () => {
        toast.current.show({ severity: 'error', summary: 'Error', detail: `${detailError}`, life: 3000 });
        setDetailError(null)
    }
    const imageBodyTemplate = (product) => {
        console.log(product);
        var a
        gifts.forEach(element => {
            if (element.id === product.giftId) {
                a = element.image
            }
        })
        return <img src={`http://localhost:5111/Gift/${a}`} alt={product.image} className="w-6rem shadow-2 border-round" />;
    };

    const header = (
        <div className="flex flex-wrap align-items-center justify-content-between gap-2">
            <span className="text-xl text-900 font-bold">Products</span>
            <Button icon="pi pi-refresh" rounded raised />
        </div>
    );

    const getGiftName = (product) => {
        var a
        gifts.forEach(element => {
            if (element.id === product.giftId) {
                a = element.name
            }
        })
        console.log("aa", a);
        return a
    }


    const onUpdate = (data) => {
        debugger
        setVisible(true)
        console.log("chose", data);
        if (accept1) {
            changeStatus(data)
        }

    }

    const changeStatus = async (data) => {
        debugger
        await changeToStatusTrue(data.id).then((response) => {
            if (!response || response.status / 100 != 2) {
                setDetailError("oopp... something error")
            }
            else {
                setDetailSuccess(`Bought!!!!`)
                setProductsNo(productsNo.filter(item => item.id !== data.id));
                setProducts(prevProducts => [...prevProducts, data])
            }
        })
    }

    // const onUpdate = async (data) => {
    //     await setVisible(true)
    //     console.log("chose", data);
    //     debugger;
    //     if (accept1) {
    //         const response = await changeToStatusTrue(data.id);
    //         if (!response || response.status / 100 !== 2) {
    //             setDetailError("Oops... something went wrong");
    //         } else {
    //             setDetailSuccess("Bought!!!!");
    //             setProductsNo(productsNo.filter(item => item.id !== data.id));
    //             setProducts(prevProducts => [...prevProducts, data]);
    //         }
    //     }
    // }
    const onDelete = async (data2) => {
        debugger
        const response = await deleteSale(data2.id)
        console.log("deleteS", response);
        if (response.status == 200) {
            setProductsNo(productsNo.filter(item => item.id !== data2.id));
        }
    }

    return (<>
        <Toast ref={toast} />

        {detailSuccess ? showSuccess() : <></>}
        {detailError ? showError() : <></>}
        <ConfirmDialog group="declarative" visible={visible} onHide={() => setVisible(false)} message="Are you sure you want to pay?"
            header="Confirmation" icon="pi pi-exclamation-triangle" accept={() => setAccept(true)} reject={() => setAccept(false)} />
        {buy ? (<div className="card">
            <Button label="Shopping cart" icon="pi pi-shopping-cart" className="w-15rem mx-auto" style={{ backgroundColor: "var(--orange-500)", borderColor: "var(--surface-400)", boxShadow: "var(--surface-400)" }} onClick={() => setBuy(false)}></Button>
            <DataTable value={products} header={header} tableStyle={{ minWidth: '60rem' }}>
                <Column field={getGiftName} header="Gift"></Column>
                <Column header="Image" body={imageBodyTemplate}></Column>
                <Column field="count" header="Number of tickets"></Column>

            </DataTable>
        </div>) : (
            <div className="card">
                <Button label="Previous purchases" icon="pi pi-user" className="w-15rem mx-auto" style={{ backgroundColor: "var(--orange-500)", borderColor: "var(--surface-400)", boxShadow: "var(--surface-400)" }} onClick={() => setBuy(true)}></Button>
                <DataTable value={productsNo} header={header} tableStyle={{ minWidth: '60rem' }}>
                    <Column field={getGiftName} header="Gift"></Column>
                    <Column header="Image" body={imageBodyTemplate}></Column>
                    <Column field="count" header="Number of tickets"></Column>
                    <Column body={(rowData) => (
                        <div className="p-grid p-align-center p-justify-center">
                            <Button onClick={() => onUpdate(rowData)} icon="pi pi-check-circle" rounded raised />
                            <Button onClick={() => onDelete(rowData)} icon="pi pi-trash" rounded raised />
                        </div>
                    )} />

                </DataTable>
            </div>
        )}
    </>);
}
