import React, { useState, useEffect } from "react";
import { api } from '../api';
import OrderForm from "./orderForm";
import '../index.css'
function AddPurchaseHeader() {
    const [submitType, setSubmitType] = useState("submit");
    const [supplierList,setSupplierList] = useState(<option>loading</option>);
    const [supplierName,setSupplierName] = useState('');
    const [selectId,setSelectId] = useState(0);
    const [supplierId,setSupplierId] = useState([1,2,3]);
    const handleSubmit = (e) => {
        let json = false;
        e.preventDefault();

        const data = new FormData(document.getElementById("orderFormHeader"))
        console.log(data);
        const url = api.API_URL + 'purchase';
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

    function disappear(e) {
        setSubmitType("hidden")
        console.log(e.target)
    }
    useEffect(() => {
        const url = api.API_URL + 'supplier';

        const fetchData = async () => {
            try {
                const response = await fetch(url);
                const json = await response.json();
                
                console.log(json);
                setSupplierList(json.map((data)=>
                    <option key={data.SupplierId}>{data.SupplierName}</option>
                ))
                setSupplierId(json.map((data)=>
                    data.SupplierId
                ))
                setSupplierName(json.map(data=>data.SupplierName))
            } catch (error) {
                console.log("error", error);
            }
        };

        fetchData();
    },[])
   
   
    return (
        <div className="uk-card uk-card-default uk-card-body uk-width-2-3@m uk-position-top-center uk-position-large">
            <h3 className="uk-card-title">訂貨單</h3>
            <form id="orderFormHeader" className="uk-form-horizontal uk-margin-large" onSubmit={handleSubmit}>

                <fieldset className="uk-fieldset">
                    <input name="SupplierId" className="uk-input" type="hidden" value={supplierId.at(selectId)} />
                    <div className="uk-margin">
                    <label className="uk-form-label" >廠商</label>
                    <select  className="uk-select uk-form-width-medium" onChange={(e)=>setSelectId(supplierName.findIndex((element)=> element === e.target.value)) }>
                    {supplierList}
                    </select>
                    </div>
                    <div className="uk-margin">
                        <label className="uk-form-label" >購買總價</label>
                        <input name="PurchaseTotal" className="uk-input uk-form-width-medium" type="number" />
                    </div>
                    <div className="uk-margin">

                        <button type={submitType} className="uk-button uk-button-default submitPos2" >確定</button>
                    </div>
                </fieldset>
            </form>
            <OrderForm ID={supplierId.at(selectId)}/>
        </div>
    )
}
export default AddPurchaseHeader;