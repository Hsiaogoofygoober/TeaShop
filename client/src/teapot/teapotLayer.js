import React, { useState, useEffect } from "react";
import { api } from '../api'
import { Link } from 'react-router-dom'

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
          console.log(data.ProductDescription)
          return (
            <div key={data.ProductId} >
              <div className="uk-card uk-card-default">
                <div className="uk-card-media-top">
                  <img src={"./productImg/" + data.ProductID} />
                </div>
                <div className="uk-card-body">
                  <h3 className="uk-card-title">{data.Name}</h3>
                  <p>{data.Type + "    " + data.ProductCategory}</p>
                  <a className="uk-button uk-button-default" uk-toggle="#modal-overflow">產品描述</a>

                  <div id="modal-overflow" uk-modal="true">
                    <div className="uk-modal-dialog">

                      <button className="uk-modal-close-default" type="button" uk-close="true"></button>

                      <div className="uk-modal-header">
                        <h2 className="uk-modal-title">產品描述</h2>
                      </div>

                      <div className="uk-modal-body" uk-overflow-auto="true">
                        <p>{data.ProductDescription}</p>
                      </div>

                    </div>
                  </div>
                </div>
              </div>
            </div>
          );
        }))
      } catch (error) {
        console.log("error", error);
      }
    };

    fetchData();
  }, [])

  return (
    <div className="uk-grid-column-small uk-grid-row-small uk-child-width-1-3@s uk-text-center" uk-grid="true" >
      {productJson}
    </div>
  )
}

export default TeapotLayer;