import React, { Component } from 'react';
class TopFooter extends Component {

    render() {
        return (
            <div className="container-fluid top--part">
                <div className="container">
                    <div className="col">
                        <div className="row">
                            <div className="col-xl-3 col-sm-12 col-md-3 wrapper">
                                <i className="fa  fa-phone photo fa-2x photo-phone"></i>
                                <span className="phone">
                                    <p className="top-footer-font-1 first-p">SALES CONTACT</p>
                                    <h4 className="top-footer-font-1">123 456 7890</h4>
                                </span>
                            </div>
                            <div className="col-xl-3 col-sm-12 col-md-3 wrapper">
                                <i className="fa fa-wrench photo fa-2x photo-wrench"></i>
                                <span>
                                    <p className="top-footer-font-1">SERVICE CONTACT</p>
                                    <h4 className="top-footer-font-1">123 456 7890</h4>
                                </span>
                            </div>
                            <div className="col-xl-3 col-sm-12 col-md-3 wrapper">
                                <i className="fa fa-cogs photo fa-2x photo-cogs"></i>
                                <span>
                                    <p className="top-footer-font-1">PARTS CONTACT</p>
                                    <h4 className="top-footer-font-1">123 456 7890</h4>
                                </span>
                            </div>
                            <div className="col-xl-3 col-sm-12 col-md-3 wrapper">
                                <i className="fa fa-check-square-o photo fa-2x photo-square"></i>
                                <span>
                                    <p className="top-footer-font-1">ONLINE APPRAISAL</p>
                                    <p className="p-schedule">Schedule online visit.</p>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default TopFooter;
