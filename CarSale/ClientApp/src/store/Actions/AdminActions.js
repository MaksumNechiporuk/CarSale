import axios from "axios";

export const actionCreators = {
    PostFilters: (value, name) => async (dispatch) => {
        const url = `api/Admin/AddFilter`;
        let item;
        await axios
            .post(url, { params: { name: name, value: value } })
            .then(response =>
                item = response.data);
        let { addedvalue, addedname } = item;
        dispatch({ type: "AddFilter", addedvalue, addedname });
        return;

    }
};