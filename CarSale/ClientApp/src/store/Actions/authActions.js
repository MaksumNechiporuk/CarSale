import axios from "axios";
import qs from "qs";

export const actionCreators = {
    Register: (data) => async (dispatch) => {
       console.log(data)
        const url = `api/Account/Registration`;
        let user;
        await axios
            .post(url,data)
            .then(response =>
                user = response.data);
       dispatch({ type: "Registration", user});
       return;
    },

     Log: (data) => async (dispatch) => {
        console.log(data)
        const url = `api/Account/Login`;
        let user;
        await axios
            .post(url, data)
            .then(response =>
                user = response.data);
        dispatch({ type: "Login", user });
        return;
    }
};