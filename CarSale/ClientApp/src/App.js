import React, { Fragment } from 'react';
import { Route } from 'react-router';
import { withRouter } from "react-router-dom";
import 'primereact/resources/themes/nova-light/theme.css';
import 'primereact/resources/primereact.min.css';
import 'primeicons/primeicons.css';
import './css/main.css'
import Header from "./components/Header/Header"
import CarListStart from "./components/CarList/CarListStart"
import CarList from "./components/CarList/CarList"

import About from "./components/About/About"
import EditProfile from "./components/Profile/EditProfile/EditProfile"
import ShowProfile from "./components/Profile/ShowProfile/ShowProfile"

import CarPost from "./components/CarList/CarItem/CarPost/CarPost"
import Registration from "./components/Registration/Registration"
import Login from "./components/Login/Login"

function App() {
	return (
		<Fragment>
			<Route
				path="/"

				render={() => (
					<Fragment>
						<Header />

					</Fragment>
				)}
			/>
			<Route
				path="/"
				exact
				render={() => (
					<Fragment>
						<CarListStart />
						<About />
					</Fragment>
				)}
            />
            <Route
                path="/Registration"
                exact
                render={(props) => (
                    <Registration {...props} />
                )}
            />
            <Route
                path="/Login"
                exact
                render={(props) => (
                    <Login {...props} />
                )}
            />
			<Route
				path="/Cars/:page"
				exact
				render={(props) => (
					<CarList {...props} />
				)}
			/>
			<Route
				path="/CarPost/:id"
				exact
				render={(props) => (
					<CarPost {...props} />
				)}
            />
		</Fragment>
	);
}
export default withRouter(App);