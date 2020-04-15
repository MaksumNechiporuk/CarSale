import React, {Fragment} from 'react';
import { Route } from 'react-router';
import './css/main.css'
import CarList from "./components/CarList/CarList"
import Footer from "./components/Footer/Footer"

export default () => (
    <Fragment>
        <CarList />
        <Footer />
    </Fragment>
);
