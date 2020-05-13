const initialState = {
    CurrentUser: {}};

export const reducer = (state, action) => {
    state = state || initialState;
    if (action.type === "Registration") {
        return Object.assign({}, state, {
            CurrentUser:action.user


        })
    };
    if (action.type === "Login") {
        return Object.assign({}, state, {
            CurrentUser: action.user


        })
    };
    return state;
};
