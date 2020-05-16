import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from "../../store/Actions/CarActions";
import { Redirect, Link } from "react-router-dom";
import CarItem from "./CarItem/CarItem"
class CarListStart extends Component {

	componentDidMount() {
		this.ensureDataFetched();
	}

	//componentDidUpdate() {
	//	this.ensureDataFetched();
	//}

	ensureDataFetched() {
		this.props.GetrequestCarList(1, 6);
	}
	render() {
		let singleItem = null;
		if (this.props.carList.carList)
			singleItem = this.props.carList.carList.map(item => {
				let path = "/CarPost/" + item.id;
				return (
					<Link to={path} key={item.id} className="unlinkCar col-xl-4 col-sm-12 col-md-6">
						<CarItem
							id={item.id}

							name={item.name}
							state={item.state}
							year={item.date}
							img={item.image}
							price={item.price}
							mileage={item.mileage}
						/>
					</Link>
				);
			});
		return (
			<div className="listCar">
				<div className="container">
					<div className="row" id="ads">
						{singleItem}
					</div>
				</div>
			</div>
		);
	}
}
const mapStateToProps = state => {
	return {
		carList: state.carList
	};
};

const mapDispatchToProps = dispatch => {
	const { GetrequestCarList } = bindActionCreators(actionCreators, dispatch);
	return {
		GetrequestCarList
	};
};

export default connect(mapStateToProps, mapDispatchToProps)(CarListStart);