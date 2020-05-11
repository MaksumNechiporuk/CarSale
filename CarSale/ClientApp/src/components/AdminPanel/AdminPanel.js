import React, { Fragment, Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from "../../store/Actions/AdminActions";

const AdminPanel = () => {


    return (
        <Fragment>
            <div className="container admin-panel">
                <div className="row">
                    <div className="col">
                        <h2 className ="filter-header"> Filters</h2>
                    
                        <div className="input-group mb-3">
                            <div className="input-group-prepend">
                                <span className="input-group-text" id="inputGroup-sizing-default">Filter Name</span>
                            </div>
                            <input type="text" className="form-control" aria-label="Default" aria-describedby="inputGroup-sizing-default" />
                            <div class="input-group-prepend">
                                <span className="input-group-text" id="inputGroup-sizing-default">Value</span>
                            </div>
                            <input type="text" className="form-control" aria-label="Default" aria-describedby="inputGroup-sizing-default" />
                        </div>
                        <button className="btn-addfilter">Add</button>
                    </div>
                </div>
            </div>
        </Fragment>
    );

}
const mapStateToProps = state => {
    return {
        carList: state.carList,
        totalPage: state.totalPage
    };
};

const mapDispatchToProps = dispatch => {
    const { GetrequestCarList } = bindActionCreators(actionCreators, dispatch);
    return {
        GetrequestCarList
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(CarList);

export default AdminPanel;

class AdminPanel extends Component {
    constructor(props) {
        super(props);
        this.state = {
            value,
            name
        };
    }
    render() {
        return (
                <Fragment>
                    <div className="container admin-panel">
                        <div className="row">
                            <div className="col">
                                <h2 className="filter-header"> Filters</h2>

                                <div className="input-group mb-3">
                                    <div className="input-group-prepend">
                                        <span className="input-group-text" id="inputGroup-sizing-default">Filter Name</span>
                                    </div>
                                    <input type="text" className="form-control" aria-label="Default" aria-describedby="inputGroup-sizing-default" />
                                    <div class="input-group-prepend">
                                        <span className="input-group-text" id="inputGroup-sizing-default">Value</span>
                                    </div>
                                    <input type="text" className="form-control" aria-label="Default" aria-describedby="inputGroup-sizing-default" />
                            </div>
                            <button className="btn-addfilter" onClick={AddFilter}>Add</button>
                            </div>
                        </div>
                    </div>
                </Fragment>
        );
    }
}
const mapStateToProps = state => {
    return {
        value: state.value,
        name: state.name
    };
};

const mapDispatchToProps = dispatch => {
    const { AddFilter } = bindActionCreators(actionCreators, dispatch);
    return {
        AddFilter
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(AdminPanel);