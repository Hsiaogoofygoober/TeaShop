import React, { useState} from "react";
import { api } from '../api';
import SalesForm from "./salesForm";
import { Link } from "react-router-dom";
import Navbar from "../navbar/navbar";
import '../index.css'
function AddSalesHeader() {
    const [submitType, setSubmitType] = useState("submit");
    const [purchaseId,setPurchseId] = useState(0);
    const handleSubmit = (e) => {
        let json = false;
        e.preventDefault();

        const data = new FormData(document.getElementById("salesFormHeader"))
        console.log(data);
        const url = api.API_URL + 'sales';
        const value = Object.fromEntries(data.entries());
        console.log(JSON.stringify(value));
        const sendOrderData = async () => {
            try {
                const response = await fetch(url, {
                    method: 'post',
                    headers: { 'Content-Type': 'application/json' },
                    body:
                        JSON.stringify(value)
                })

                json = await response.json();
                console.log(value);
                console.log(response);
                console.log(json);
                setPurchseId(json);
            }
            catch (error) {
                console.log("error", error);
            }
        }
        sendOrderData();
    }

   
    return (
        <div>
        <Navbar/>
        <div className="uk-card uk-card-default uk-card-body uk-width-2-3@m uk-position-top-center uk-position-large uk-position-relative">
            <p><Link to="/Sales" uk-icon="icon:  arrow-left; ratio: 1.5"></Link> <span className="uk-card-title uk-position-medium uk-position-top-center">出貨單</span></p>
           
            <form id="salesFormHeader" className="uk-form-horizontal uk-margin-large" onSubmit={handleSubmit}>

                <fieldset className="uk-fieldset">
                    
                    <div className="uk-margin">
                    <label className="uk-form-label" >客戶</label>
                    <input name="Customer" className="uk-input uk-form-width-medium" type="text" />
                    
                    </div>
                    <div className="uk-margin">
                        <label className="uk-form-label" >出售總價</label>
                        <input name="SalesTotal" className="uk-input uk-form-width-medium" type="number" />
                    </div>
                    <div className="uk-margin">

                        <button type={submitType} className="uk-button uk-button-default submitPos2" >確定</button>
                    </div>
                </fieldset>
            </form>
            <SalesForm ID={purchaseId}/>
        </div>
        </div>
    )
}
export default AddSalesHeader;