import React, { useState, useEffect } from 'react';
import { Button } from 'primereact/button';
import { DataView } from 'primereact/dataview';
import { classNames } from 'primereact/utils';
import { changeToStatusTrue, getAllGifts } from './axios/giftAxios'
import { useNavigate } from 'react-router-dom';
import { getAllCategories } from './axios/categoryAxios'
import { RandomCustomer } from './axios/saleAxios';
import { Toast } from 'primereact/toast';
import { useRef } from 'react';
import { addWinner } from './axios/winnerAxios';


export default function Lottery(props) {
    const [products, setProducts] = useState([]);
    const [categories, setCategories] = useState([]);
    const [errorMessage, setErrorMessage] = useState(false);
    const [successMessage, setSuccessMessage] = useState(false);
    const toast = useRef(null);
    const [layout, setLayout] = useState('grid');
    const [visible, setVisible] = useState(false);
    const [value2, setValue2] = useState(false);
    const [selectedCity, setSelectedCity] = useState(null);
    const [currentProduct, setCurrentProduct] = useState(null);
    const [zochim, setZochim] = useState([]);
    const navigate = useNavigate()

    const save=async()=>{
        debugger
        const gift={
            "Id":currentProduct.id,
            "Price":value2,
            "CategoryId":selectedCity.id
        }
        await changeToStatusTrue(gift).then((response)=>console.log(response.data78))
        setVisible(false)
        navigate('/');
    }

    const footerContent = (
        <div>
            <Button label="No" icon="pi pi-times" onClick={() => setVisible(false)} className="p-button-text" />
            <Button label="Yes" icon="pi pi-check" onClick={() => save()} autoFocus />
        </div>
    );

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
    const showError = () => {
        debugger
        toast.current.show({ severity: 'error', summary: 'Error', detail: "please login", life: 3000 });
        setErrorMessage(false)
    }
    const showSuccess = () => {
        debugger
        toast.current.show({ severity: 'success', summary: 'Success', detail: `the zoche is ${zochim[zochim.length-1].name}`, life: 3000 });
        setSuccessMessage(false)
    }
    const Delete=async (product)=>{
        
    }

    const lotto=async(product)=>{
        const customer=await RandomCustomer(product.id)
        debugger
        if(customer.status==200){
            const res=await addWinner({"GiftId":product.id,"CustomerId":customer.data.id})
            if(res.status==200){
                setZochim(prevProducts => [...prevProducts, customer.data])
                setSuccessMessage(true)
            }
        }
        
        console.log("zochim",zochim);
        // setVisible(true)
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
                            <Button icon="pi pi-crown" className="p-button-rounded" disabled={product.inventoryStatus === 'OUTOFSTOCK'} onClick={() => lotto(product)}></Button>

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

    return (<>
        <Toast ref={toast} />
        {/* <div className="card flex justify-content-center">
            <Dialog header="Header"
             visible={visible}
              style={{ width: '50vw' }} 
              onHide={() => { if (!visible) return; setVisible(false); }}
               footer={footerContent}
               >
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
        </div> */}

        {/* <ConfirmDialog /> */}
        {errorMessage && showError()}
        {successMessage && showSuccess()}
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
