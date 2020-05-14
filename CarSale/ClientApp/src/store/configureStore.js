import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import thunk from 'redux-thunk';
import { routerReducer, routerMiddleware } from 'react-router-redux';
import * as Counter from './Counter';
import * as WeatherForecasts from './WeatherForecasts';
import * as CarList from './CarList';
import * as UserStore from './UserStore';

export default function configureStore (history, initialState) {
  const reducers = {
    counter: Counter.reducer,
    weatherForecasts: WeatherForecasts.reducer,
      carList: CarList.reducer,
      UserStore: UserStore.reducer
  };

import * as Filters from './Filters';

export default function configureStore(history, initialState) {
	const reducers = {
		counter: Counter.reducer,
		weatherForecasts: WeatherForecasts.reducer,
		carList: CarList.reducer,
		filters: Filters.reducer
	};


	const middleware = [
		thunk,
		routerMiddleware(history)
	];

	// In development, use the browser's Redux dev tools extension if installed
	const enhancers = [];
	const isDevelopment = process.env.NODE_ENV === 'development';
	if (isDevelopment && typeof window !== 'undefined' && window.devToolsExtension) {
		enhancers.push(window.devToolsExtension());
	}

	const rootReducer = combineReducers({
		...reducers,
		routing: routerReducer
	});

	return createStore(
		rootReducer,
		initialState,
		compose(applyMiddleware(...middleware), ...enhancers)
	);
}
