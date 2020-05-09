import axios from "axios";

export const actionCreators = {
	GetrequestCarList: startDateIndex => async (dispatch) => {
		const url = `api/AppUser/GetUsers`;
		let users;
		await axios
			.get(url)
			.then(response =>
				users = response.data);
		console.log(users);
		dispatch({ type: "GetUsers", users });
		return;

	}
};