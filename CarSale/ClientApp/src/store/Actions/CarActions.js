import axios from "axios";

export const actionCreators = {
	GetrequestCarList: startDateIndex => async (dispatch) => {
		const url = `api/Car/GetCars`;
		let cars;
		await axios
			.get(url)
			.then(response =>
				cars = response.data);
		console.log(cars);
		dispatch({ type: "GetCars", cars });
		return;

	}
};