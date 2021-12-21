import React,{useState} from "react";
import SignUpForm from './signUpForm';
import img from '../img/teapot.jfif'
const imgStyle = {
    width: 'auto',
    height: 'auto' 
  };
function SignUp() {
 

    return (
        <div className="uk-child-width-1-4@m uk-margin-medium-top center">
            <div className="uk-height-medium">
                <div className="uk-card uk-card-default">
                    <div className="uk-card-media-top">
                        <img src={img} style={imgStyle}/>
                    </div>
                    <SignUpForm />
                    
                </div>
            </div>
        </div>
    )
}
export default SignUp;