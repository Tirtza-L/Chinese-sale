import React, { useState, useEffect } from 'react';
import { Button } from 'primereact/button';
import { DataView, DataViewLayoutOptions } from 'primereact/dataview';
import { classNames } from 'primereact/utils';
import { addGift, changeToStatusTrue, deleteGift, getAllGifts, getAllNoGifts } from './axios/giftAxios'
import { addCategory, getCategoryById, getCategoryByName } from './axios/categoryAxios'
import { useLocation, useNavigate } from 'react-router-dom';
import { getAllCategories } from './axios/categoryAxios'
import { addSale } from './axios/saleAxios';
import { jwtDecode } from 'jwt-decode';
import { Toast } from 'primereact/toast';
import { useRef } from 'react';
import { ConfirmDialog, confirmDialog } from 'primereact/confirmdialog';
import { Dialog } from 'primereact/dialog';
import { InputNumber } from 'primereact/inputnumber';
import { InputText } from 'primereact/inputtext';
import { FloatLabel } from 'primereact/floatlabel';
import { CascadeSelect } from 'primereact/cascadeselect';


export default function TableAllGifts(props) {
    const [products, setProducts] = useState([]);
    const [categories, setCategories] = useState([]);
    const [errorMessage, setErrorMessage] = useState(false);
    const [dialog, setDialog] = useState(false);
    const toast = useRef(null);
    const [layout, setLayout] = useState('grid');
    const [visible, setVisible] = useState(false);
    const [valueName, setValueName] = useState(null);
    const [valueDesc, setValueDesc] = useState(null);
    const [value2, setValue2] = useState(false);
    const [selectedCity, setSelectedCity] = useState(null);
    const [currentProduct, setCurrentProduct] = useState(null);
    const [newCategory, setNewCategory] = useState(null);
    const navigate = useNavigate()
    const [detailError, setDetailError] = useState(null);
    const [detailSuccess, setDetailSuccess] = useState(null);

    const showSuccess = () => {
        toast.current.show({ severity: 'success', summary: 'Success', detail: `${detailSuccess}`, life: 3000 });
        setDetailSuccess(null)
    }

    const showError = () => {
        toast.current.show({ severity: 'error', summary: 'Error', detail: `${detailError}`, life: 3000 });
        setDetailError(null)
    }


    const save = async () => {
        if (!selectedCity) {
            setDetailError("Please choose a category")
            debugger
            return
        }
        debugger
        const gift = {
            "Id": currentProduct.id,
            "Name": valueName,
            "Description": valueDesc,
            "Price": value2,
            "CategoryId": selectedCity.id
        }
        await changeToStatusTrue(gift).then((response) => {
            debugger
            if (response.status / 100 == 2) {
                setDetailSuccess("sucsseccful")
                setTimeout(() => {
                    setVisible(false)
                }, 3000);
            }
            else {
                setDetailError("something Wrong:(")
                setTimeout(() => {
                    setVisible(false)
                }, 3000);

            }
            
            navigate('/');
        })

    }

    const footerContent = (
        <div>
            <Button label="No" icon="pi pi-times" onClick={() => setVisible(false)} className="p-button-text" />
            <Button label="Yes" icon="pi pi-check" onClick={() => save()} autoFocus />
        </div>
    );

    const getAll = async () => {
        await getAllNoGifts().then((data) => setProducts(data));
    }

    useEffect(() => {
        console.log("props", props);
        const asyncFetchDailyData = async () => {
            await getAll()
        }
        asyncFetchDailyData();
    }, []);

    useEffect(() => {
        const asyncFetchDailyData = async () => {
            const a = await getAllCategories()
            setCategories(a)
        }
        asyncFetchDailyData();
    }, []);


    const accept = () => {
        toast.current.show({ severity: 'info', summary: 'Confirmed', detail: 'You have accepted', life: 3000 });
    }

    const reject = () => {
        <Button>vggg</Button>
        toast.current.show({ severity: 'warn', summary: 'Rejected', detail: 'You have rejected', life: 3000 });
    }

    console.log("gifts", products)

    const getCategoryName = (id) => {
        var a
        categories.forEach(element => {
            if (element.id === id) {
                a = element.name
            }
        })
        console.log("aa", a);
        return a
    }


    const Delete = async (product) => {
        debugger
        await deleteGift(product.id).then((data) => {
            debugger
            if (!data || data.status / 100 != 2) {
                setDetailError("something Wrong:(,\n maybe this gift bought already??")
            }
            else {
                setDetailSuccess("the gift deleted")
            }
        })
    }

    const openDialog = (product) => {
        setCurrentProduct(product)
        debugger
        if (product.categoryId) {
            setValue2(product.price)
            setValueName(product.name)
            setValueDesc(product.description)
            setSelectedCity(getCategoryName(product.categoryId))
        }
        setVisible(true)
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
                                    {console.log("product.categoryId", product.categoryId)}
                                    <span className="font-semibold">{getCategoryName(product.categoryId)}</span>
                                </span>
                            </div>
                        </div>
                        <div className="flex sm:flex-column align-items-center sm:align-items-end gap-3 sm:gap-2">
                            <span className="text-2xl font-semibold">${product.price}</span>
                            <Button icon="pi pi-check" className="p-button-rounded" disabled={product.inventoryStatus === 'OUTOFSTOCK'} onClick={() => openDialog(product)}></Button>
                            <Button icon="pi pi-trash" className="p-button-rounded" onClick={() => Delete(product)}></Button>

                        </div>

                    </div>
                </div>
            </div>
        );
    };


    const itemTemplate = (product, layout, index) => {
        if (!product) {
            return;
        }

        return listItem(product, index);

    };

    const listTemplate = (products, layout) => {
        return <div className="grid grid-nogutter">{products.map((product, index) => itemTemplate(product, layout, index))}</div>;
    };

    const AddNewCategory = async () => {
        await addCategory({ "Name": newCategory }).then((data) => {
            debugger
            if (data.status / 100 == 2) {
                setCategories(prevProducts => [...prevProducts, data.data])
                setDetailSuccess("Category added successfully")
            }
            else {
                setDetailError("Somthing wrong, please try again")
            }
        })
    }

    return (<>
        <Toast ref={toast} />
        {detailSuccess ? showSuccess() : <></>}
        {detailError ? showError() : <></>}
        <InputText onBlur={(e) => setNewCategory(e.target.value)}></InputText>
        <Button onClick={() => AddNewCategory()}>Add new catagory</Button>
        <div className="card flex justify-content-center">
            <Dialog header="Editing details"
                visible={visible}
                style={{ width: '50vw' }}
                onHide={() => { if (!visible) return; setVisible(false); }}
                footer={footerContent}
            >
                <p className="m-0">
                    <div className="flex-auto">
                        <label htmlFor="horizontal-buttons" className="font-bold block mb-2">Name</label>
                        <InputText inputId="horizontal-buttons" value={valueName} onChange={(e) => setValueName(e.value)} />
                    </div>

                </p>
                <p className="m-0">
                    <div className="flex-auto">
                        <label htmlFor="horizontal-buttons" className="font-bold block mb-2">Description</label>
                        <InputText inputId="descriptin" value={valueDesc} onChange={(e) => setValueDesc(e.target.value)} />
                    </div>
                </p>
                <p className="m-0">
                    <div className="flex-auto">
                        <label htmlFor="horizontal-buttons" className="font-bold block mb-2">price</label>
                        <InputNumber min={20} inputId="horizontal-buttons" value={value2} onValueChange={(e) => setValue2(e.value)} showButtons buttonLayout="horizontal" step={20.00}
                            decrementButtonClassName="p-button-danger" incrementButtonClassName="p-button-success" incrementButtonIcon="pi pi-plus" decrementButtonIcon="pi pi-minus"
                            mode="currency" currency="USD" />
                    </div>

                </p>
                <br></br>
                <div className="flex-auto">
                    <FloatLabel>
                        <CascadeSelect inputId="cs-city" value={selectedCity} onChange={(e) => setSelectedCity(e.value)} options={categories}
                            optionLabel="name" optionGroupLabel="name" optionGroupChildren={['states', 'cities']}
                            className="w-full md:w-14rem" breakpoint="767px" style={{ minWidth: '14rem' }} />
                        <label htmlFor="cs-city">Category</label>
                    </FloatLabel>
                </div>
            </Dialog>
        </div>

        {/* <ConfirmDialog /> */}
        {errorMessage && showError()}
        <div className="card">
            {products ?
                <DataView
                    value={products}
                    listTemplate={listTemplate}
                    layout={layout}
                /> : <></>}
        </div>
    </>
    )
}
