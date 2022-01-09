import React from "react";
import {api} from '../api';
import { useNavigate } from "react-router-dom";
function AddSupplier() {
    let navigate = useNavigate()
    const handleAddSupplierSubmit = (e) => {
        let json = false;
        e.preventDefault();

        const data = new FormData(document.getElementById("addSupplier"))
        console.log(data);
        const url = api.API_URL + 'Supplier';
        const value = Object.fromEntries(data.entries());
        console.log(JSON.stringify(value));
        const sendAddSupplier = async () => {
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
        sendAddSupplier();
        navigate('/Purchase/orderFormHeader');
    }


    return (
        <div className="uk-card uk-card-default uk-card-body uk-width-1-2@m">
            <h3 className="uk-card-title">新增廠商</h3>
            <form id="addSupplier" onSubmit={
                handleAddSupplierSubmit}>
                <fieldset className="uk-fieldset">
                    <div className="uk-margin">
                        <label className="uk-form-label">廠商:</label>
                        <input name="SupplierName" className="uk-input" type="text" />
                    </div>
                    <div className="uk-margin">
                        <button className="uk-button uk-button-default" type="submit" >確定</button>
                    </div>
                </fieldset>
            </form>
        </div>

    )
}

export default AddSupplier