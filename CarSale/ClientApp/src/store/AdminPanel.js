
const initialState = {
    value, name
};

export const reducer = (state, action) => {
    state = state || initialState;
    if (action.type === "AddFilter") {
        return Object.assign({}, state, {
            value: action.value,
            name: action.name


        })
    }
    return state;
};
