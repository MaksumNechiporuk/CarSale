import React, { Fragment } from 'react';
import { Route } from 'react-router';
import './css/main.css'
import CarList from "./components/CarList/CarList"
import EditProfile from "./components/Profile/EditProfile/EditProfile"
import ShowProfile from "./components/Profile/ShowProfile/ShowProfile"

export default () => (
	<Fragment>
		<CarList />
		<EditProfile />
		<ShowProfile />
	</Fragment>
);
