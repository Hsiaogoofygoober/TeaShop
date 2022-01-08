import React, { useState, useEffect } from "react";
import { api } from '../api';
import { Link } from "react-router-dom";
import '../index.css';
import DeleteSales from "./deleteSales";
import Navbar from "../navbar/navbar";
function Sales() {
  const [total, setTotal] = useState(0);
  const [checkDelete,setCheckDelete] = useState(0);
  const [purchaseDetail, setpurchaseDetail] = useState(<tr><td>loading</td></tr>);
  const [purchase, setpurchase] = useState(<tr><td>loading</td></tr>);
  const [ID, setID] = useState(0);
  const url = api.API_URL + 'Sales';
  console.log(purchaseDetail);
  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(url);
        const json = await response.json();
       
        setpurchase(json.map((data, index) => {
          const date = new Date(data.SalesDate);
          const month = date.getMonth()+1;
          const dateString = date.getFullYear() + "-" +month+ "-" + date.getDate();
          return (

            <tr key={data.SalesOrderId}>
              <td>{data.SalesOrderId}</td>
              <td>{dateString}</td>
              <td>{data.Customer}</td>
              <td>{data.SalesTotal}</td>
              <td>
                <a className="uk-button uk-button-default" uk-toggle={"target: #ID" + data.SalesOrderId} onClick={() => setID(data.SalesOrderId)}>訂單詳情</a>
                <div id={"ID" + data.SalesOrderId} uk-modal="true" className="uk-modal-container" >
                  <div className="uk-modal-dialog ">

                    <button className="uk-modal-close-default" type="button" uk-close="true"></button>

                    <div className="uk-modal-header">
                      <p>出貨單編號 : {data.SalesOrderId}</p>
                      <p>出貨日期 : {dateString}</p>
                      <p>客戶 : {data.Customer}</p>
                    </div>

                    <div className="uk-modal-body" uk-overflow-auto="true">
                      <table className="uk-table uk-table-hover uk-table-divider" >
                        <thead>
                          <tr>
                            <th>產品代號</th>
                            <th>產品分類</th>
                            <th>產品類別</th>
                            <th>產品名稱</th>
                            <th>數量</th>
                            <th>單價</th>
                            <th>單項總價</th>
                          </tr>
                        </thead>
                        <tbody>
                          {purchaseDetail}
                        </tbody>
                      </table>
                      <hr className=" uk-position-medium"/>
                      
                      <p className="uk-position-bottom-right uk-position-medium priceFont">原定總價 : {total}</p>
                      <p className="uk-position-bottom-right uk-position-medium priceFont">折扣價 :<span className="priceColor">{data.SalesTotal}</span> </p>
                    </div>

                  </div>
                </div>
              </td>

              <td><a href="" uk-icon="pencil"></a></td>
              <td><button uk-icon="ban" onClick={()=>{
                DeleteSales(data.SalesOrderId)
          
                }}></button></td>

            </tr>

          )
        }))
      } catch (error) {
        console.log("error", error);
      }
    };
    fetchData();

  }, [purchaseDetail])

  useEffect(() => {
    const getPurchaseDetail = async () => {
      try {
        setTotal(0);
        console.log(url + "/" + ID);
        const res = await fetch(url + "/" + ID);
        const detailJson = await res.json();
        console.log(detailJson);
        setpurchaseDetail(
          detailJson.map((detailData, index) => {
            setTotal(total => total + detailData.SODVM.UnitPrice * detailData.SODVM.SalesQuantity);

            return (
              <tr key={index}>
                <td>{detailData.PVM.ProductId}</td>
                <td>{detailData.PVM.Type}</td>
                <td>{detailData.PVM.ProductCategory}</td>
                <td>{detailData.PVM.Name}</td>
                <td>{detailData.SODVM.SalesQuantity}</td>
                <td>{detailData.SODVM.UnitPrice}</td>
                <td>{detailData.SODVM.UnitPrice * detailData.SODVM.SalesQuantity}</td>
              </tr>
            )
          }
          )
        )


      }
      catch (error) {
        console.log("error", error);
      }
    }
    getPurchaseDetail();


  }, [ID])

  useEffect(() => {
    console.log(total);
  }, [total])

  return (
    <div>
    <Navbar/>
    <div className="uk-card uk-card-default uk-card-body uk-width-1-2@m uk-position-top-center uk-position-large uk-position-relative">
      <p><span className="priceFont">所有出貨單</span><span  className="uk-position-top-right uk-position-medium"><Link to="salesFormHeader" className="plus_cycle"  uk-icon="icon: plus-circle; ratio: 1.5"></Link></span></p>
      <table className="uk-table uk-table-hover uk-table-divider">
        <thead>
          <tr>
            <th>出貨單編號</th>
            <th>日期</th>
            <th>客戶</th>
            <th>總價錢</th>
            <th>出貨單詳情</th>
            <th>修改</th>
            <th>刪除</th>
          </tr>
        </thead>
        <tbody>
          {purchase}
        </tbody>
      </table>
    </div>
    </div>
  )

}

export default Sales;