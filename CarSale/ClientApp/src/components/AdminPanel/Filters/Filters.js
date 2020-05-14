import React, { Fragment, useState } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { actionCreators } from "../../../store/Actions/AdminActions";

const Filters = ({ AddFilter }) => {

   

   const [inputFields, setInputFields] = useState([
    { value: ''}
  ]);

   

  const handleInputChange = (index, event) => {
    const values = [...inputFields];
   
      values[index].value = event.target.value;
   
    setInputFields(values);
  };

    const handleSubmit = e => {
        e.preventDefault();
        const data = new FormData(e.target);
        let value = inputFields.map((item) => {
            return item.value
        });
        console.log("value", value);
        let send_data = {
            name: data.get("name"), values: value
        };
        AddFilter(send_data);
    };

  const handleAddFields = () => {
    const values = [...inputFields];
    values.push({ value: ''});
    setInputFields(values);
  };
 
    return (
        <Fragment>
            <div className="container admin-panel">
                
          <div className="row">
            <div className="col">
              <h2 className="filter-header"> Filters</h2>

                        <form onSubmit={handleSubmit}>
                <div className="input-group mb-3">
                  <div className="input-group-prepend">
                    <span
                      className="input-group-text"
                      id="inputGroup-sizing-default"
                    >
                      Filter Name
                    </span>
                  </div>
                  <input
                    id="name"
                    name="name"
                    type="text"
                    className="form-control"
                    aria-label="Default"
                    aria-describedby="inputGroup-sizing-default"
                  />
                  <div class="input-group-prepend">
                    <span
                      className="input-group-text"
                      id="inputGroup-sizing-default"
                    >
                      Filter Value
                    </span>
                  </div>
                   {inputFields.map((inputField, index) => (
            <Fragment key={`${inputField}~${index}`}>
              <div className="form-group col-sm-6">
                <label htmlFor="value">Value</label>
                <input
                  type="text"
                  className="form-control"
                  onChange={event => handleInputChange(index, event)}
                  id="value"
                  name="value"
                  value={inputField.value}
                />
              </div>
            </Fragment>
           ))}
                </div>
                <button type="button" className="btn-addfilter"  onClick={() => handleAddFields()}>
                  Add new value
                </button>
                <button type="submit" className="btn-addfilter">
                  Add
                </button>
              </form>
            </div>
          </div>
        </div>
      </Fragment>
    );
}


const mapDispatchToProps = (dispatch) => {
    const { AddFilter } = bindActionCreators(actionCreators, dispatch);
    return {
        AddFilter
    };
};

const mapStateToProps = (state) => {
  return {
    value: state.value,
    name: state.name,
  };
};



export default connect(mapStateToProps, mapDispatchToProps)(Filters);
