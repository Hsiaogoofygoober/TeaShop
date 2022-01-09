import React from "react";
import '../index.css'
import img1 from '../img/teapot3.jpg'
import img2 from '../img/tea4.png'
function Intro() {

    return (
        <div className="uk-child-width-1-1@m" uk-grid="true">
            <div>
                <div className="uk-card uk-card-default">
                    <div className="uk-card-media-top">
                        <img src={img1} className="backgroundImg1" />
                    </div>
                    <div className="uk-card-body">
                        <h3 className="uk-card-title">店家歷史</h3>
                        <p>流傳自中國幾千年歷史的技藝，來自老師傅的技術傳承，原封不動地移到台
                            南古都來...</p>
                    </div>
                </div>
            </div>
            <div>
                <div className="uk-card uk-card-default">
                    <div className="uk-card-media-top">
                        <img src={img2} className="backgroundImg2" />
                    </div>
                    <div className="uk-card-body">
                        <h3 className="uk-card-title">店家簡介</h3>
                        <p>老闆頂著30年的買賣經歷，來到店面裡應有盡有，不過隨著疫情爆發的
                            影響，到店面光顧的客人愈趨減少，因此希望能藉由網路的力量，看到老店面跟著
                            時代一起做的改變，歡迎各位線上瀏覽後，能到本店大駕光臨!</p>
                    </div>
                </div>
                <div className="uk-card uk-card-default uk-card-body uk-width-1-1@m">
                    <h3 class="uk-card-title">店家產品介紹</h3>
                </div>
            </div>
        </div>
    )
}

export default Intro;