import React, { Fragment } from 'react';
import { Route } from 'react-router';
import { withRouter, Redirect } from "react-router-dom";

import 'primereact/resources/themes/nova-light/theme.css';
import 'primereact/resources/primereact.min.css';
import 'primeicons/primeicons.css';
import './css/main.css'
import Header from "./components/Header/Header"
import CarListStart from "./components/CarList/CarListStart"
import CarList from "./components/CarList/CarList"
import Footer from "./components/Footer/Footer"
import About from "./components/About/About"
import EditProfile from "./components/Profile/EditProfile/EditProfile"
import ShowProfile from "./components/Profile/ShowProfile/ShowProfile"
import NotFound from "./components/NotFound/NotFound"
import CarPost from "./components/CarList/CarItem/CarPost/CarPost"
import AdminPanel from "./components/AdminPanel/AdminPanel"
import Filters from "./components/AdminPanel/Filters/Filters"
import InProgress from "./components/AdminPanel/InProgress/InProgress"
import AdvancedSearch from "./components/AdvancedSearch/AdvancedSearch"
import AddCar from './components/AddCar/AddCar';

function App() {
	return (
		<Fragment>
			<Route
				path="/"

				render={(props) => (
					<Fragment>
						<Header {...props} />

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
				path="/Cars/:page"

				render={(props) => (
					<CarList {...props} />
				)}
            />
            <Route
                path="/AdminPanel/filters"
                exact
                render={() => (
                    <Fragment>
                        <AdminPanel />
                        <Filters />
                    </Fragment>

                )}
            />
            <Route
                path="/AdminPanel/inprogress"
                exact
                render={() => (
                    <Fragment>
                        <AdminPanel />
                        <InProgress />
                    </Fragment>

                )}
            />
			<Route
				path="/CarPost/:id"
				exact
				render={(props) => (
					<CarPost {...props} />
				)}

			/>	
			<Route
				path="/AdvancedSearch"
				exact
				render={(props) => (
					<AdvancedSearch {...props} />
				)}
			/>	
			<Route
				path="/AddCar"
				exact
				render={(props) => (
					<AddCar {...props} />
				)}
			/>	
		</Fragment>
	);
}
export default withRouter(App);

