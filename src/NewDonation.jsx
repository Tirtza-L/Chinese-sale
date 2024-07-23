import React, { useState, useRef } from "react";
import { Button } from 'primereact/button';
import { Dialog } from 'primereact/dialog';
import { InputText } from "primereact/inputtext";
import { FloatLabel } from "primereact/floatlabel";
import { Toast } from 'primereact/toast';
import { jwtDecode } from "jwt-decode";
import { addGift } from "./axios/giftAxios";

export default function NewDonation(props) {
    const toast = useRef(null);
    const [visible, setVisible] = useState(false);
    const [visibleName, setVisibleName] = useState(false);
    const [visibleDescription, setVisibleDescription] = useState(false);
    const [visibleImage, setVisibleImage] = useState(false);
    const [value1, setValue1] = useState('');
    const [value2, setValue2] = useState('');
    const [Image, setImage] = useState(null);

     

    const handleImageChange = (e) => {
        setImage(e.target.files[0]);
    };

    const wantToSave = async () => {
        var flag = true
        debugger
        if (value1 === '') {
            setVisibleName(true)
            flag = false
        }
        else {
            setVisibleName(false)
        }
        if (value2 === '') {
            setVisibleDescription(true)
            flag = false
        }
        else {
            setVisibleDescription(false)
        }
        if (Image === null) {
            flag = false
            setVisibleImage(true)
        }
        else{
            setVisibleImage(false)
            var a=Image.name.split(".")
            a=a[a.length-1].toUpperCase()
            if(a!="JPG"&&a!="JPEG"&&a!="PNG"&&a!="SVG"&&a!="GIF"&&a!="WEBP"){
                flag = false
                setVisibleImage(true)
            }
        }
       
        if (flag) {
            const token = localStorage.getItem("token")
            const decodedToken = jwtDecode(token)
            debugger
            const id = parseInt(decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"])
            const formData = new FormData();
            formData.append('CustomerId', id);
            formData.append('name', value1);
            formData.append('description', value2);
            formData.append('image', Image);

            try {
                const response = await addGift(formData);
                console.log("responseAddGift",response);
                if(response.status==200){
                    onHideReset()
                    props.setDetailSuccess("The gift added successfully, thank you!!")
                }
                else{
                    onHideReset()
                    props.setDetailError("Something problem, please try again")

                }
                // Handle success response
            } catch (error) {
                console.error(error);
                onHideReset()

                props.setDetailError("Something problem, please try again")
                // Handle error
            }
        };
    }

    const footerContent = (
        <div>
            <Button label="Cencel" severity="danger" icon="pi pi-times" onClick={() =>onHideReset()} className="p-button-text" />
            <Button label="Save" severity="success" icon="pi pi-check" onClick={() => wantToSave()} className="p-button-text" autoFocus />
        </div>
    );

    const onHideReset=()=>{
        debugger
        props.setVisible(false)
        setVisibleImage()
        setImage(null)
        setValue1('')
        setValue2('') 
    }

    return (
        <div className="card flex justify-content-center">
            <Toast ref={toast} />
            {/* <Button style={{ backgroundColor: "var(--orange-500)", borderColor: "var(--surface-400)", boxShadow: "var(--surface-600)" }} label="SignUp" icon="pi pi-user-plus" onClick={() => setVisible(true)} /> */}
            <Dialog header="Donation" visible={props.visible} onHide={() => onHideReset()}
                style={{ width: '30vw' }} breakpoints={{ '960px': '75vw', '641px': '100vw' }} footer={footerContent}>
                {/* {isLoading ? <ProgressSpinner style={{ position: "center" }} /> : */}
                <p className="m-0">
                    <br></br>
                    <div className="card flex justify-content-center">
                        <FloatLabel>
                            <InputText id="name" value={value1} onChange={(e) => setValue1(e.target.value)} />
                            <label htmlFor="name">*Name of gift</label>
                        </FloatLabel>
                    </div>

                    {visibleName ? <small className="card flex justify-content-center" id="username-help" style={{ color: "red" }} >
                        Name is required
                    </small> : <></>}
                    <br />
                    <div className="card flex justify-content-center">
                        <FloatLabel>
                            <InputText id="description" value={value2} onChange={(e) => setValue2(e.target.value)} />
                            <label htmlFor="description">*Description about gift</label>
                        </FloatLabel>
                    </div>

                    {visibleDescription ? <small className="card flex justify-content-center" id="password-help" style={{ color: "red" }} >
                        Description is required
                    </small> : <></>}
                    <br></br>

                    <div className="card flex justify-content-center">
                        <input id="image" type="file" accept="image/*" onChange={handleImageChange} />
                    </div>
                    {visibleImage ? <small className="card flex justify-content-center" id="username-help" style={{ color: "red" }} >
                        Not valid image
                    </small> : <></>}
                    <br />
                </p>
            </Dialog>
        </div>
    )
}