import React, { useState, useEffect } from "react";
import '../index.css'

function AddPurchase(props) {
    const [amount, setAmount] = useState(0);
    const [unitPrice, setUnitPrice] = useState(0);
    const [name, setName] = useState('紫砂提梁大壺');
    const [category, setCategory] = useState('綠泥');
    const [type, setType] = useState('茶壺');
    const [productId, setProductId] = useState(1);
    const [productName, setproductName] = useState(
        <option>loading</option>
    )
    const [productCategory, setProductCategory] = useState(
        <optgroup>
            <option>綠泥</option>
            <option>朱泥</option>
            <option>紫泥</option>
            <option>鍛泥</option>
        </optgroup>);


    function changeCategory() {
        if (document.getElementById("type" + props.index).value === "茶壺") {
            setProductCategory(
                <optgroup>
                    <option>綠泥</option>
                    <option>朱泥</option>
                    <option>紫泥</option>
                    <option>鍛泥</option>
                </optgroup>
            )
        }
        else {
            setProductCategory(
                <optgroup>

                    <option>紅茶</option>
                    <option>綠茶</option>
                    <option>青茶</option>
                    <option>黑茶</option>
                </optgroup>

            )
        }

    }
    function getProductName(){
        setproductName(props.productJson.map((data, index) => {
            if(data.Type === type && data.ProductCategory === category){
                return (
                    <option key={index}>{data.Name}</option>
                )
            }
        }))
    }

    function getProductId() {
        console.log(type)
        console.log(category)
        console.log(name)
        if (props.productJson.find((data) =>

            data.Type === type &&
            data.ProductCategory === category &&
            data.Name === name

        ) !== undefined) {
            setProductId(props.productJson.find((data) =>

                data.Type === type &&
                data.ProductCategory === category &&
                data.Name === name

            ).ProductId)
           
        }
        else{
            setProductId('')
        }


    }
    /*useEffect(() => {
        if(document.getElementById("Name" + props.index).value!==undefined){
            setCategory(document.getElementById("Name" + props.index).value)
        }
        console.log(document.getElementById("Name" + props.index).value)
    
    }, [category])*/
    useEffect(()=>{
        getProductName()
        
    },[type,category])
    useEffect(() => {
        /*if(document.getElementById("type" + props.index).value!==undefined){
            setCategory(document.getElementById("type" + props.index).value)
        }
        if(document.getElementById("ProductCategory" + props.index).value!==undefined){
            setCategory(document.getElementById("ProductCategory" + props.index).value)
        }*/
        
        getProductId()
    }, [type, name, category])

    

    return (
        <div className="uk-grid-medium uk-child-width-expand@s " uk-grid="true">
            <div className="inline">
                <label className="uk-form-label">產品代號 :</label>
                <input name={"ProductId"+props.index} className="uk-input" type={"number"} type="hidden" value={productId} />
            </div>
            <div className="inline">
                <label className="uk-form-label">產品分類 :</label>

                <select id={"type" + props.index} className="uk-select" onChange={(e) => {
                    console.log(e.target.value)
                    setType(e.target.value)
                    changeCategory()
                }
                }>
                    <option value="茶壺">茶壺</option>
                    <option value="茶葉">茶葉</option>
                </select>
            </div>
            <div className="inline">

                <label className="uk-form-label">產品類別 : </label>
                <select className="uk-select" id={"ProductCategory" + props.index} onChange={(e) => {
                    setCategory(e.target.value)
                }}>
                    {productCategory}
                </select>

            </div>
            <div className="inline" >
                <label className="uk-form-label">產品名稱 : </label>
                <select className="uk-select" id={"Name" + props.index} onChange={(e) => {
                    setName(e.target.value)
                }}>
                    {productName}
                </select>
            </div>
            <div  className="inline" >
                <label className="uk-form-label">數量 : </label>
                <input name={"PurchaseQuantity"+props.index} id={"amount" + props.index} className="uk-input" type={"number"} onChange={(e) => setAmount(e.target.value)} />
            </div>
            <div className="inline" >
                <label className="uk-form-label">單價 : </label>
                <input  name={"UnitPrice"+props.index} id={"unitPrice" + props.index} className="uk-input" type={"number"} onChange={(e) => setUnitPrice(e.target.value)} />
            </div>
            <div className="inline" >
                <label className="uk-form-label">單項總價 : </label>
                <input className="uk-input" type={"number"} disabled={true} value={amount * unitPrice} />
            </div>
            <div className="inline">

                <button id={props.index} onClick={(event) => props.setId(event.target.id)} type="button" className="deletePos uk-button uk-button-default">刪除</button>
            </div>

        </div>
    )

}

export default AddPurchase;
