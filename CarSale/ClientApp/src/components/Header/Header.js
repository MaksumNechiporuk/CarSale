import React, { Component, Fragment } from 'react';
import NavbarMenu from "./NavbarMenu/NavbarMenu"
import HeaderSearch from "./HeaderSearch/HeaderSearch"
class Header extends Component {
   
    render() {
        return (
            <Fragment>
                <NavbarMenu />
                <HeaderSearch />
            </Fragment>
        );
    }
}

export default Header;