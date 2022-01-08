import React, { useState, useEffect } from "react";
import { api } from '../api';
import { Link } from "react-router-dom";
import '../index.css';
import DeletePurchase from "./deletePurchase";
function Purchase() {
  const [total, setTotal] = useState(0);
  const [checkDelete,setCheckDelete] = useState(0);
  const [purchaseDetail, setpurchaseDetail] = useState(<tr><td>loading</td></tr>);
  const [purchase, setpurchase] = useState(<tr><td>loading</td></tr>);
  const [ID, setID] = useState(0);
  const url = api.API_URL + 'Purchase';
  console.log(purchaseDetail);
  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(url);
        const json = await response.json();
       
        setpurchase(json.map((data, index) => {
          const date = new Date(data.PurchaseDate);
          const month = date.getMonth()+1;
          const dateString = date.getFullYear() + "-" +month+ "-" + date.getDate();
          return (

            <tr key={data.PurchaseOrderId}>
              <td>{data.PurchaseOrderId}</td>
              <td>{dateString}</td>
              <td>{data.Supplier.SupplierName}</td>
              <td>{data.PurchaseTotal}</td>
              <td>
                <a className="uk-button uk-button-default" uk-toggle={"target: #ID" + data.PurchaseOrderId} onClick={() => setID(data.PurchaseOrderId)}>訂單詳情</a>
                <div id={"ID" + data.PurchaseOrderId} uk-modal="true" className="uk-modal-container" >
                  <div className="uk-modal-dialog ">

                    <button className="uk-modal-close-default" type="button" uk-close="true"></button>

                    <div className="uk-modal-header">
                      <p>進貨單編號 : {data.PurchaseOrderId}</p>
                      <p>訂購日期 : {dateString}</p>
                      <p>供應商 : {data.Supplier.SupplierName}</p>
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
                      <p className="uk-position-bottom-right uk-position-medium priceFont">折扣價 :<span className="priceColor">{data.PurchaseTotal}</span> </p>
                    </div>

                  </div>
                </div>
              </td>

              <td><a href="" uk-icon="pencil"></a></td>
              <td><button uk-icon="ban" onClick={()=>{
                DeletePurchase(data.PurchaseOrderId)
          
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
            setTotal(total => total + detailData.PodVM.UnitPrice * detailData.PodVM.PurchaseQuantity);

            return (
              <tr key={index}>
                <td>{detailData.PVM.ProductId}</td>
                <td>{detailData.PVM.Type}</td>
                <td>{detailData.PVM.ProductCategory}</td>
                <td>{detailData.PVM.Name}</td>
                <td>{detailData.PodVM.PurchaseQuantity}</td>
                <td>{detailData.PodVM.UnitPrice}</td>
                <td>{detailData.PodVM.UnitPrice * detailData.PodVM.PurchaseQuantity}</td>
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
    <div className="uk-card uk-card-default uk-card-body uk-width-1-2@m uk-position-top-center uk-position-large">
      <p><span className="priceFont">所有訂貨單</span><span  className="uk-position-top-right uk-position-medium"><Link to="orderFormHeader" className="plus_cycle"  uk-icon="icon: plus-circle; ratio: 1.5"></Link></span></p>
      <table className="uk-table uk-table-hover uk-table-divider">
        <thead>
          <tr>
            <th>訂貨單編號</th>
            <th>日期</th>
            <th>供應商</th>
            <th>總價錢</th>
            <th>訂單詳情</th>
            <th>修改</th>
            <th>刪除</th>
          </tr>
        </thead>
        <tbody>
          {purchase}
        </tbody>
      </table>
    </div>
  )

}

export default Purchase;