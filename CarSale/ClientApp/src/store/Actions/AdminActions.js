import axios from "axios";
import qs from "qs";

export const actionCreators = {
    AddFilter: (filters) => async (dispatch) => {
        const url = `api/Admin/AddFilter`;
        let item;
        await axios
            .post(url,filters
            )
            .then(response =>
                item = response.data);
        return;

    }
};