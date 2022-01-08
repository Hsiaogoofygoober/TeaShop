import React from "react";
import { Link } from "react-router-dom";

function Navbar() {

    return (
        <nav className="uk-navbar-container uk-margin" uk-navbar="mode: click">
            <div className="uk-navbar-left">

                <ul className="uk-navbar-nav">
                    <li><Link to={"/Sales"}>出貨單</Link></li>
                    <li><Link to={"/Purchase"}>訂貨單</Link></li>
                    <li><Link to={"/Stock"}>商品庫存</Link></li>
                </ul>

            </div>
        </nav>
    )

}

export default Navbar