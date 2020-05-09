import axios from "axios";

export const actionCreators = {
	GetrequestCarList: (page, count) => async (dispatch) => {
		const url = `api/Car/GetCars`;
		let item;
		await axios
			.get(url, { params: { page: page, count: count } })
			.then(response =>
				item = response.data);
		let { cars, countPage } = item;
		dispatch({ type: "GetCars", cars, countPage });
		return;

	},
	GetCarById: id => async (dispatch) => {
		const url = `api/Car/CarsById`;
		let car;

		await axios
			.get(url, { params: { id: id } })
			.then(response =>
				car = response.data);
		dispatch({ type: "GetCarsById", car });
		return;

	}
};