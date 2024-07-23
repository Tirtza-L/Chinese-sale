import React, { useState } from "react";
import { Button } from 'primereact/button';
import { Dialog } from 'primereact/dialog';
import { Divider } from 'primereact/divider';
import { InputText } from 'primereact/inputtext';
import { Password } from 'primereact/password';
import Register from './Register';
import axios from 'axios';
import { Login } from "./axios/customerAxios";
import { jwtDecode } from 'jwt-decode' // import dependency
import { useNavigate } from 'react-router-dom';


export default function Login2(props) {
    const [visible, setVisible] = useState(false);
    const navigate = useNavigate()

    var name = "", password = ""

    const login = async () => {
        try {
            debugger
            const response = await Login(name, password)
            console.log("responseH", response);
            if (response.status === 200) {
                let token = response.data
                if (token) {
                    props.setDetailSuccess("you connection successfully")
                    axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
                     const decodedToken = jwtDecode(token);
                     console.log("decodedToken", decodedToken);
                    localStorage.setItem("token",token) 
                    props.setUser(decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"])
                    props.setRole(decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"])
                    setVisible(false)
                    navigate('/')
                }
            }
            else {
                props.setDetailError("Data problem, please try again")
                setVisible(false)
            }
        }
        catch {
            props.setDetailError("Data problem, please try again")
            setVisible(false)
        }
    }

    return (
        <div className="card flex justify-content-center">
            
            <Button label="Login" icon="pi pi-user" onClick={() => setVisible(true)} style={{ backgroundColor: "gray", borderColor: "var(--surface-400)", boxShadow: "var(--surface-100)" }} />
            <Dialog header="person area"
                style={{ width: '50vw', textAlign: "center" }}
                visible={visible}
                onHide={() => setVisible(false)}
            >

                <div className="card" style={{ backgroundColor: "var(--surface-300)" }}>
                    <div className="flex flex-column md:flex-row">
                        <div className="w-full md:w-5 flex flex-column align-items-center justify-content-center gap-3 py-5">
                            <div className="card flex justify-content-center">
                                <span className="p-float-label">
                                    <InputText id="username" style={{ width: "100%", boxShadow: "var(--surface-400)", borderColor: "var(--surface-400)" }} onBlur={(e) => name = e.target.value} />
                                    <label htmlFor="username">Username</label>
                                </span>
                            </div>
                            <br />
                            <div className="card flex justify-content-center">
                                <span className="p-float-label">
                                    <Password id="password" feedback={false} style={{ boxShadow: "var(--surface-400)", borderColor: "var(--surface-400)" }} toggleMask onBlur={(e) => password = e.target.value} />
                                    <label htmlFor="password">Password</label>
                                </span>
                            </div>
                            <Button label="Login" icon="pi pi-user" className="w-10rem mx-auto" style={{ backgroundColor: "var(--orange-500)", borderColor: "var(--surface-400)", boxShadow: "var(--surface-400)" }} onClick={() => login()}></Button>
                        </div>
                        <div className="w-full md:w-2">
                            <Divider layout="vertical" className="hidden md:flex" >
                                <b  >OR</b>
                            </Divider>
                        </div>
                        <div className="w-full md:w-5 flex align-items-center justify-content-center py-5">
                            <Register
                               setDetailError={props.setDetailError}
                               setDetailSuccess={props.setDetailSuccess}
                                setVisible={setVisible}
                            ></Register>
                        </div>
                    </div>
                </div>
            </Dialog>
        </div>
    )
}