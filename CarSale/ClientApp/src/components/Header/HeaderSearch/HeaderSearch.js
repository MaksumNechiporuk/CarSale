﻿import React, { Component } from 'react';
class HeaderSearch extends Component {
   
    render() {
        return (
            <div class="container-fluid header-search ">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-4 offset-lg-8 col-md-5 offset-md-8">
                            <div class="search-part">
                                <h2 class="header-text">Search Pre-Owned Cars</h2>
                                <form class="pre-owned-form">
                                    <div class="select-group">
                                        <select class="select-item">
                                            <option hidden value=""> Make</option>
                                            <option value=""> Audi</option>
                                            <option value=""> BMV</option>
                                        </select>
                                        <select class="select-item">
                                            <option hidden value=""> Model</option>
                                            <option value=""> Audi</option>
                                            <option value=""> BMV</option>
                                        </select>
                                        <select class="select-item">
                                            <option hidden value=""> Year</option>
                                            <option value=""> Audi</option>
                                            <option value=""> BMV</option>
                                        </select>
                                        <select class="select-item">
                                            <option hidden value=""> Body Type</option>
                                            <option value=""> Audi</option>
                                            <option value=""> BMV</option>
                                        </select>
                                        <select class="select-item">
                                            <option hidden value=""> Min Price</option>
                                            <option value=""> Audi</option>
                                            <option value=""> BMV</option>
                                        </select>
                                        <select class="select-item">
                                            <option hidden value=""> Max Price</option>
                                            <option value=""> Audi</option>
                                            <option value=""> BMV</option>
                                        </select>
                                        <select class="select-item">
                                            <option hidden value=""> Color</option>
                                            <option value=""> Audi</option>
                                            <option value=""> BMV</option>
                                        </select>
                                        <button type="submit" class=" btn btn_search">Search</button>
                                        <button type="submit" class=" btn btn_reset">reset</button>
                                    </div>
                                    <div class="buttons-group">
                                        <button type="submit" class=" btn btn_buy-car">
                                            <img src="../../img/car-png.png" class="btn_buy-car-image" />
                                            Buy <br />
                                            Used Cars
                                        </button>
                                        <button type="submit" class=" btn btn_sell-car">
                                            <img src="../../img/key-png.png" class="btn_sell-car-image" />
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