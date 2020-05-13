import axios from "axios";
import qs from "qs"
export const actionCreators = {
	GetRequestMakes: () => async (dispatch) => {
		const url = `api/Filters/GetMakes`;
		let makes;
		await axios
			.get(url)
			.then(response =>
				makes = response.data);
		dispatch({ type: "GetMakes", makes });
		return;
	},
	GetModelsByMakes: (id) => async (dispatch) => {
		const url = `api/Filters/GetModelsByMakes/`;
		let models;
		await axios
			.get(url, {
				params: {
					id
				},
				paramsSerializer: params => {
					return qs.stringify(params)
				}
			})
			.then(response =>
				models = response.data);

		dispatch({ type: "GetModelsByMakes", models });
		return;
	}
	,
	GetFiltersByName: (name) => async (dispatch) => {
		const url = `api/Filters/GetFiltersByName`;
		let children;
		await axios
			.get(url, { params:{name:name} })
			.then(response =>
				children = response.data);
		console.log(children);
		switch (name) {
			case "Color":
				dispatch({ type: "SetColor", children });
				break;
			case "Fuel":
				dispatch({ type: "SetFuel", children }); break;
			case "Type of car":
				dispatch({ type: "SetTypeCar", children }); break;
			case "State":
				dispatch({ type: "SetState", children }); break;
		}
		return;

	}
};