



const initialState = {
	makes: [],
	models: [],
	stateCar: [],
	color: [],
	fuel: [],
	typeOfCar:[]

};

export const reducer = (state, action) => {
	state = state || initialState;
	if (action.type === "GetMakes") {
		return Object.assign({}, state, {
			makes: action.makes
		})
	}
	if (action.type === "GetModelsByMakes") {
		return Object.assign({}, state, {
			models: action.models
		})
	}

	if (action.type === "SetColor") {
		return Object.assign({}, state, {
			color: action.children
		
		})
	}
	if (action.type === "SetFuel") {
		return Object.assign({}, state, {
			fuel: action.children

		})
	}
	if (action.type === "SetTypeCar") {
		return Object.assign({}, state, {
			typeOfCar: action.children

		})
	}
	if (action.type === "SetState") {
		return Object.assign({}, state, {
			stateCar: action.children

		})
	}

	return state;
};