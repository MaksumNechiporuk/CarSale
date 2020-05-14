import React from "react";
const BotFooter = () => {
    return (
        <div className="container-fluid bot--part">
            <div className="container">
                <div className="row">
                    <div className="col-md-4 wrapper_b">
                        <p className="bot-footer-font-1 ">
                            Copyright © 2020 All rights reserved.
                            </p>
                    </div>
                    <div className="col-md-4 wrapper_b">
                        <p className="bot-footer-font-1 ">
                            <a href="#" className="bot-footer-font-1">Text & Conditons</a>
                            <a href="#" className="bot-footer-font-2">Privacy Policy </a>
                        </p>
                    </div>
                    <div className="col-md-4 wrapper_b">
                        <p className="bot-footer-font-1 ">
                            Follow Us :
                                <a href="#" className="bot-footer-font-1 fa fa-facebook-square photo-face fa-lg "></a>
                            <a href="#" className="bot-footer-font-1 fa fa-twitter-square photo-face fa-lg "></a>
                            <a href="#" className="bot-footer-font-1 fa fa-instagram photo-face fa-lg "></a>
                            <a href="#" className="bot-footer-font-1 fa fa-google-plus-square photo-face fa-lg "></a>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default BotFooter;