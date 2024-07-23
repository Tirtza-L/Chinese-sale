
import React, { useState, useEffect } from 'react';
import { Button } from 'primereact/button';
import { DataView, DataViewLayoutOptions } from 'primereact/dataview';
import { classNames } from 'primereact/utils';
import { getAllGifts } from './axios/giftAxios'
import { addSale } from './axios/saleAxios';
import { jwtDecode } from 'jwt-decode';
import { Toast } from 'primereact/toast';
import { useRef } from 'react';
import { getWinnerByGift } from './axios/winnerAxios';

export default function Table(props) {
    const [products, setProducts] = useState([]);
    const [errorMessage, setErrorMessage] = useState(false);
    const toast = useRef(null);
    const [layout, setLayout] = useState('grid');
    const [detailError, setDetailError] = useState(null);
    const [detailSuccess, setDetailSuccess] = useState(null);

    const getAll = async () => {
        await getAllGifts().then((data) => setProducts(data));
    }

    useEffect(() => {
        console.log("props", props);
        const asyncFetchDailyData = async () => {
            await getAll()
        }
        asyncFetchDailyData();
    }, []);

    console.log("gifts", products)

    const showSuccess = () => {
        toast.current.show({ severity: 'success', summary: 'Success', detail: `${detailSuccess}`, life: 3000 });
        setDetailSuccess(null)
    }

    const showError = () => {
        toast.current.show({ severity: 'error', summary: 'Error', detail: `${detailError}`, life: 3000 });
        setDetailError(null)
    }
    const addCart = async (product) => {

        const token = localStorage.getItem("token")
        if (token == null) {
            setDetailError("please login")
            return;
        }

        const decodedToken = jwtDecode(token)
        var id = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"]

        const saleDto = {
            "CustomerId": Number(id),
            "GiftId": Number(product.id)
        }
        await addSale(saleDto).then((data) => {
            debugger
            if (!data || data.status / 100 != 2) {
                setDetailError("oopp... something error")
            }
            else {
                setDetailSuccess(`${product.name} added to cart!!`)
            }
        })


    }
    const listItem = (product, index) => {
        return (
            <div className="col-12" key={product.id}>
                <div className={classNames('flex flex-column xl:flex-row xl:align-items-start p-4 gap-4', { 'border-top-1 surface-border': index !== 0 })}>
                    <img className="w-9 sm:w-16rem xl:w-10rem shadow-2 block xl:block mx-auto border-round" src={`http://localhost:5111/Gift/${product.image}`} alt={product.name} />
                    <div className="flex flex-column sm:flex-row justify-content-between align-items-center xl:align-items-start flex-1 gap-4">
                        <div className="flex flex-column align-items-center sm:align-items-start gap-3">
                            <div className="text-2xl font-bold text-900">{product.name}</div>
                            <div className="text-xl font text-400">{product.description}</div>
                            <div className="flex align-items-center gap-3">
                                <span className="flex align-items-center gap-2">
                                    <i className="pi pi-tag"></i>
                                    <span className="font-semibold">{product.category1.name}</span>
                                </span>
                                <span className="flex align-items-center gap-2">
                                    <i className="pi pi-gift"></i>
                                    <span className="font-semibold">{product.customer.name}</span>
                                </span>
                            </div>
                        </div>
                        <div className="flex sm:flex-column align-items-center sm:align-items-end gap-3 sm:gap-2">
                            <span className="text-2xl font-semibold">${product.price}</span>
                            {!product.hasWinner ? <Button icon="pi pi-cart-plus" className="p-button-rounded" disabled={product.inventoryStatus === 'OUTOFSTOCK'} onClick={() => addCart(product)}></Button>
                                  :  <div className="font-semibold">{ getWinnerByGift(product.id)}</div>
                            }
                        </div>
                    </div>
                </div>
            </div>
        );
    };

    const gridItem = (product) => {
        return (
            <div className="col-12 sm:col-6 lg:col-12 xl:col-4 p-2" key={product.id}>
                <div className="p-4 border-1 surface-border surface-card border-round">
                    <div className="flex flex-wrap align-items-center justify-content-between gap-2">
                        <div className="flex align-items-center gap-2">
                            <i className="pi pi-tag"></i>
                            <span className="font-semibold">{product.category1.name}</span>
                        </div>
                        <div className="flex align-items-center gap-2">
                            <i className="pi pi-gift"></i>
                            <span className="font-semibold">{product.customer.name}</span>
                        </div>
                    </div>
                    <div className="flex flex-column align-items-center gap-3 py-5">
                        <img className="w-9 shadow-2 border-round" src={`http://localhost:5111/Gift/${product.image}`} alt={product.name} />
                        <div className="text-2xl font-bold">{product.name}</div>
                        <div className="text-xl font text-400">{product.description}</div>
                    </div>
                    <div className="flex align-items-center justify-content-between">
                        <span className="text-2xl font-semibold">${product.price}</span>
                        <Button icon="pi pi-cart-plus" className="p-button-rounded" disabled={product.inventoryStatus === 'OUTOFSTOCK'} onClick={() => addCart(product)}></Button>
                    </div>
                </div>
            </div>
        );
    };

    const itemTemplate = (product, layout, index) => {
        if (!product) {
            return;
        }

        if (layout === 'list') return listItem(product, index);
        else if (layout === 'grid') return gridItem(product);
    };

    const listTemplate = (products, layout) => {
        return <div className="grid grid-nogutter">{products.map((product, index) => itemTemplate(product, layout, index))}</div>;
    };

    const header = () => {
        return (
            <div className="flex justify-content-end" >
                <DataViewLayoutOptions layout={layout} onChange={(e) => setLayout(e.value)} />
            </div>
        );
    };

    return (<>

        <Toast ref={toast} />
        {detailSuccess ? showSuccess() : <></>}
        {detailError ? showError() : <></>}
        <div className="card">
            {products ?
                <DataView
                    value={products}
                    listTemplate={listTemplate}
                    layout={layout} header={header()}
                /> : <></>}
        </div>
    </>
    )
}
