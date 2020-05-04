import React, { Component } from 'react';
class HeaderSearch extends Component {
   
    render() {
        return (
            <div className="container-fluid header-search ">
                <div className="container">
                    <div className="row">
                        <div className="col-lg-4 offset-lg-8 col-md-5 offset-md-8">
                            <div className="search-part">
                                <h2 className="header-text">Search Pre-Owned Cars</h2>
                                <form className="pre-owned-form">
                                    <div className="select-group">
                                        <select className="select-item">
                                            <option hidden value=""> Make</option>
                                            <option value=""> Audi</option>
                                            <option value=""> BMV</option>
                                        </select>
                                        <select className="select-item">
                                            <option hidden value=""> Model</option>
                                            <option value=""> Audi</option>
                                            <option value=""> BMV</option>
                                        </select>
                                        <select className="select-item">
                                            <option hidden value=""> Year</option>
                                            <option value=""> Audi</option>
                                            <option value=""> BMV</option>
                                        </select>
                                        <select className="select-item">
                                            <option hidden value=""> Body Type</option>
                                            <option value=""> Audi</option>
                                            <option value=""> BMV</option>
                                        </select>
                                        <select className="select-item">
                                            <option hidden value=""> Min Price</option>
                                            <option value=""> Audi</option>
                                            <option value=""> BMV</option>
                                        </select>
                                        <select className="select-item">
                                            <option hidden value=""> Max Price</option>
                                            <option value=""> Audi</option>
                                            <option value=""> BMV</option>
                                        </select>
                                        <select className="select-item">
                                            <option hidden value=""> Color</option>
                                            <option value=""> Audi</option>
                                            <option value=""> BMV</option>
                                        </select>
                                        <button type="submit" className=" btn btn_search">Search</button>
                                        <button type="submit" className=" btn btn_reset">reset</button>
                                    </div>
                                    <div className="buttons-group">
                                        <button type="submit" className=" btn btn_buy-car">
                                            <img src="../../img/car-png.png" className="btn_buy-car-image" />
                                            Buy <br />
                                            Used Cars
                                        </button>
                                        <button type="submit" className=" btn btn_sell-car">
                                            <img src="../../img/key-png.png" className="btn_sell-car-image" />
                                            Sell <br />
                                            Your Car
                                        </button>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        );
    }
}

export default HeaderSearch;