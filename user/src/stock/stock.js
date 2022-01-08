import React, { useState, useEffect } from "react";
import { api } from '../api'
import Navbar from "../navbar/navbar";
function Stock() {
    const [stock, setStock] = useState();
    useEffect(() => {
        const url = api.API_URL + 'stock';

        const fetchData = async () => {
            try {
                const response = await fetch(url);
                const json = await response.json();

                console.log(json);
                setStock(json.map((data,index) =>
                    <tr key={data.PVM.ProductId}>
                        <td>{index+1}</td>
                        <td>{data.PVM.ProductId}</td>
                        <td>{data.PVM.Type}</td>
                        <td>{data.PVM.ProductCategory}</td>
                        <td>{data.PVM.Name}</td>
                        <td>{data.SVM.StockAmount}</td>
                    </tr>
                ))

            } catch (error) {
                console.log("error", error);
            }
        };

        fetchData();
    }, [])

    return (
        <div>
            <Navbar />
            <div className="uk-card uk-card-default uk-card-body uk-width-1-2@m uk-position-top-center uk-position-large uk-position-relative">
                <p><span className="priceFont">現有庫存</span><span className="uk-position-top-right uk-position-medium"></span></p>
                <table className="uk-table uk-table-hover uk-table-divider">
                    <thead>
                        <tr>
                            <th>編號</th>
                            <th>產品代號</th>
                            <th>產品分類</th>
                            <th>產品類別</th>
                            <th>產品名稱</th>
                            <th>庫存數量</th>
                        </tr>
                    </thead>
                    <tbody>
                        {stock}
                    </tbody>
                </table>
            </div>
        </div>
    )
}

export default Stock;