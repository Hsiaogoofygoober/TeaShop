import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import SignIn from './signIn/signIn';
import SignUp from './signUp/signUp';
import Purchase from './purchase/purchase';

import AddPurchaseHeader from './purchase/addPurchaseHeader';
import Sales from './sales/sales';
import AddSalesHeader from './sales/addSalesHeader';
//import Stock from './stock/stock';

import {
  BrowserRouter,
  Routes,
  Route
} from "react-router-dom";


ReactDOM.render(
  <BrowserRouter>
  <Routes>
  <Route path="/" element={<SignIn/>} />
  <Route path="SignUp" element={<SignUp/>} />
  <Route path="Purchase" element={<Purchase/>} />
  <Route path="Purchase/orderFormHeader" element={<AddPurchaseHeader/>} />
  <Route path="Sales" element={<Sales/>} />
  <Route path="Sales/salesFormHeader" element={<AddSalesHeader/>} />
  </Routes>
  </BrowserRouter>
  ,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
//reportWebVitals();
