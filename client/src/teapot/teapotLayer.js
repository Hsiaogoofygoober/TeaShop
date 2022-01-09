import React, { useState, useEffect } from "react";
import { api } from '../api'
import defaultImg from '../img/default.jpg'
import '../index.css'

function TeapotLayer() {

  const url = api.API_URL + 'Product';
  const [productJson, setProductJson] = useState('loading');
  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(url, {
          method: 'get',
          headers: { 'Content-Type': 'application/json' }
        });
        const json = await response.json();
        console.log(json)
        setProductJson(json.map((data) => {
          console.log(data.ProductPicture)

          return (
            <li data-tags={data.Type + " " + data.ProductCategory + " " + "全部"} key={data.ProductId}>
              <div>
                <div className="uk-card uk-card-default">
                  <div className="uk-card-media-top">
                    <img src={data.ProductPicture} className="productImg" onError={(e) => { e.target.onerror = null; e.target.src = defaultImg }} />
                  </div>
                  <div className="uk-card-body">
                    <p className="uk-card-title productNameFont" >{data.Name}</p>
                    <p>{data.Type + "    " + data.ProductCategory}</p>
                    <a className="uk-button uk-button-default" uk-toggle={"target: #ID" + data.ProductId}>產品描述</a>

                    <div id={"ID" + data.ProductId} uk-modal="true">
                      <div className="uk-modal-dialog">

                        <button className="uk-modal-close-default" type="button" uk-close="true"></button>

                        <div className="uk-modal-header">
                          <h2 className="uk-modal-title">{data.Name}</h2>
                        </div>

                        <div className="uk-modal-body" uk-overflow-auto="true">
                          <p>{data.ProductDescription}</p>
                        </div>

                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </li>
          );
        }))
      } catch (error) {
        console.log("error", error);
      }
    };

    fetchData();
  }, [])

  return (
    <div className="uk-container uk-container-small ">
      <div uk-filter="target: .js-filter" >
        <ul className="uk-subnav uk-subnav-pill">
          <li uk-filter-control="[data-tags*='全部']" ><a href="#">全部</a></li>
          <li><a href="#">茶壺</a>
            <div className="uk-navbar-dropdown" uk-dropdown="mode: click">
              <ul className="uk-nav uk-navbar-dropdown-nav">
                <li uk-filter-control="[data-tags*='茶壺']"><a href="#">所有品種</a></li>
                <li uk-filter-control="[data-tags*='綠泥']"><a href="#">綠泥</a></li>
                <li uk-filter-control="[data-tags*='朱泥']"><a href="#">朱泥</a></li>
                <li uk-filter-control="[data-tags*='紫泥']"><a href="#">紫泥</a></li>
                <li uk-filter-control="[data-tags*='鍛泥']"><a href="#">鍛泥</a></li>
              </ul>
            </div>
          </li>
          <li><a href="#">茶葉</a>
            <div className="uk-navbar-dropdown" uk-dropdown="mode: click">
              <ul className="uk-nav uk-navbar-dropdown-nav">
                <li uk-filter-control="[data-tags*='茶葉']"><a href="#">所有品種</a></li>
                <li uk-filter-control="[data-tags*='綠茶']"><a href="#">綠茶</a></li>
                <li uk-filter-control="[data-tags*='青茶']"><a href="#">青茶</a></li>
                <li uk-filter-control="[data-tags*='紅茶']"><a href="#">紅茶</a></li>
                <li uk-filter-control="[data-tags*='黑茶']"><a href="#">黑茶</a></li>
              </ul>
            </div>
          </li>
        </ul>
        <ul className="js-filter uk-grid-column-small uk-grid-row-small uk-child-width-1-4@s uk-text-center" uk-grid="true" >
          {productJson}
        </ul>
      </div>
    </div>
  )
}

export default TeapotLayer;