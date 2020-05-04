import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from "../../store/Actions/CarActions";

import CarItem from "./CarItem/CarItem"
class CarList extends Component {

	componentDidMount() {
		this.ensureDataFetched();
	}

	//componentDidUpdate() {
	//	this.ensureDataFetched();
	//}

	ensureDataFetched() {
		this.props.GetrequestCarList(1);
	}



	render() {
		const singleItem = this.props.carList.carList.map(item => {

			return (
				<CarItem
					id={item.id}
					key={item.id}
					name={item.name}
					state={item.state}
					year={item.date}
					img={item.image}
					price={item.price}
					mileage={item.mileage}
				/>
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

export default connect(mapStateToProps, mapDispatchToProps)(CarList);