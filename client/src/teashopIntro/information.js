import React from "react";
import '../index.css'
import img3 from '../img/user.png'
function Information() {

    return (
        <div className="uk-card uk-card-default uk-width-1-2@m uk-position-medium uk-position-bottom-center uk-position-relative">
            <div className="uk-card-header">
                <div className="uk-grid-small uk-flex-middle" uk-grid="true">
                    <div className="uk-width-auto">
                        <img className="uk-border-circle" width="100" height="100" src={img3} />
                    </div>
                    <div className="uk-width-expand">
                        <h3 className="uk-card-title uk-margin-remove-bottom">林先生</h3>
                    </div>
                </div>
            </div>
            <div className="uk-card-body">
                <p>聯絡地址: 台南市中西區大新街3號</p>
            </div>
        </div>
    )
}

export default Information;