import React, { useState, useEffect } from "react";
import AddPurchase from "./addPurchase";
import '../index.css'
import { api } from '../api'
function OrderForm(props) {
 
    const [inputList, setInputList] = useState([]);
    const [id, setId] = useState(0);
    const [key, setKey] = useState(0);
    
    const [productJson,setProductJson] = useState();
    const urlProduct = api.API_URL + 'Product';


    const onAddBtnClick = (event) => {

        setKey((key) => key + 1);
        setInputList(
            inputList.concat(
                <div key={key}>
                    <AddPurchase index={key} setId={setId} productJson={productJson}/>
                </div>
            )
        );
    };
    const DeleteBtn = () => {
        var toRemove = id;

        var temp = inputList;
        console.log(inputList);
        temp = temp.filter(function (item) {
            return item.key !== toRemove;
        });
        setInputList(temp);
    };

    const handleSubmit =(e)=>{
        let json = false;
        e.preventDefault();
        
        const data = new FormData(document.getElementById("orderForm"))
        console.log(data);
        const url = api.API_URL + 'purchase/'+props.ID;
        const value = Object.fromEntries(data.entries());
        console.log(JSON.stringify(value));
        const sendOrderData = async()=>{
            try{
                const response = await fetch(url, { 
                    method: 'post', 
                    headers: {'Content-Type': 'application/json'},
                    body:
                    JSON.stringify(value)
                    })
                    
                json = await response.json();
                console.log(value);
                console.log(response);
                console.log(json);
            }
            catch(error){
                console.log("error",error);
            }
        }
        sendOrderData();
        }
    
    useEffect(() => {
        const fetchProductData = async () => {
            try {
                const response = await fetch(urlProduct);
                const json = await response.json();
                setProductJson(json);
                
            }
            catch (error) {
                console.log("error", error);
            }
        }

        fetchProductData();
    }, [])
    useEffect(() => {
        DeleteBtn()
        console.log(inputList)
    }, [id]);

    return (
        
            <form id="orderForm" className="uk-form-horizontal uk-margin-large"  onSubmit={handleSubmit}>
               
                <fieldset className="uk-fieldset">
                        
                    <div>
                        <div>{inputList}</div>
                        <button onClick={() => onAddBtnClick()} type="button" className="uk-button uk-button-secondary uk-position-small addPos">新增欄位</button>
                        <button type="submit" className="uk-button uk-button-default submitPos" >送出</button>
                    </div>
                </fieldset>
            </form>
        

    )
}

export default OrderForm;