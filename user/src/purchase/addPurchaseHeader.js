import React,{useState,useEffect} from "react";
import { api } from '../api';
import OrderForm from "./orderForm";
import '../index.css'
function AddPurchaseHeader() {
    const [submitType,setSubmitType] = useState("submit");
    const handleSubmit = (e) => {
        let json = false;
        e.preventDefault();

        const data = new FormData(document.getElementById("orderFormHeader"))
        console.log(data);
        const url = api.API_URL + 'Login';
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
            }
            catch (error) {
                console.log("error", error);
            }
        }
        sendOrderData();
    }

   
    return (
        <div className="uk-card uk-card-default uk-card-body uk-width-2-3@m uk-position-top-center uk-position-large">
            <h3 className="uk-card-title">訂貨單</h3>
            <form id="orderFormHeader" className="uk-form-horizontal uk-margin-large" onSubmit={handleSubmit}>

                <fieldset className="uk-fieldset">

                    <div className="uk-margin">
                        <label className="uk-form-label" >廠商</label>
                        <input className="uk-input uk-form-width-medium" type="text" />
                    </div>
                    <div className="uk-margin">
                    <label className="uk-form-label" >購買總價</label>
                    <input className="uk-input uk-form-width-medium" type="number" />
                    </div>
                    <div className="uk-margin">

                        <button type={submitType} className="uk-button uk-button-default submitPos" >確定</button>
                    </div>
                </fieldset>
            </form>
            <OrderForm/>
        </div>
    )
}
export default AddPurchaseHeader;