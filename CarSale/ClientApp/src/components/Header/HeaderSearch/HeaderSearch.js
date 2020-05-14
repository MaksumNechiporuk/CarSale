import React, { Component, Fragment } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from "../../../store/Actions/FiltersActions";
import { MultiSelect } from 'primereact/multiselect';
import { InputNumber } from 'primereact/inputnumber';
import { actionCreators as carActions } from "../../../store/Actions/CarActions";
import { withRouter } from "react-router";
import { Link } from 'react-router-dom';

class HeaderSearch extends Component {

	state = {
		make: [],
		model: [],
		color: [],
		StateCar: [],
		typeOfCar: [],
		maxPrice: null,
		minPrice: null
	};
	componentDidMount() {
		this.ensureDataFetched();
		this.props.GetFiltersByName("State");
		this.props.GetFiltersByName("Color");
		this.props.GetFiltersByName("Fuel");
		this.props.GetFiltersByName("Type of car");
	}
	async	getModels() {
		await this.props.GetModelsByMakes(this.state.make);
	}
	async	ensureDataFetched() {
		await this.props.GetRequestMakes();
	}
	CreateOptions = (propName) => {
		let items;
		switch (propName) {
			case "color":
				items = this.props.color;
				break;
			case "fuel":
				items = this.props.fuel;
				break;
			case "Type of car":
				items = this.props.typeOfCar;
				break;
			case "State":
				items = this.props.stateCar;
				break;
		}
		if (items.length != 0) {
			let options = items.children.map(
				item => {
					return ({
						label: `${item.name}`,
						value: `${item.id}`

					})
				});
			return options;
		}
	}
	onSubmit = event => {
		event.preventDefault();
		let { model, color, StateCar, typeOfCar, make, maxPrice, minPrice } = this.state;
		let filters = [...model, ...color, ...StateCar, ...typeOfCar];
		this.props.GetrequestCarList(1, 9, filters, make, maxPrice, minPrice);
		let path = `/Cars/1`;
		this.props.history.push(path);
	};
	resetForm = event => {
		event.preventDefault();
		this.setState({
			make: [],
			model: [],
			color: [],
			StateCar: [],
			typeOfCar: [],
			maxPrice: null,
			minPrice: null
		}, async () => {
			let { model, color, StateCar, typeOfCar } = this.state;
			let filters = [...model, ...color, ...StateCar, ...typeOfCar];
			console.log("filters:", filters);
			await this.props.GetrequestCarList(1, 9, filters);
		});
		let path = `/Cars/1`;
		this.props.history.push(path);
	};

	render() {
		let make = [], model = [];

		if (this.props.makes) {
			make = this.props.makes.map(item => {
				return ({
					label: `${item.name}`,
					value: `${item.id}`

				})
			});
			if (this.props.models)
				model = this.props.models.map(item => {
					return ({
						label: `${item.name}`,
						value: `${item.id}`

					})
				});
		}

		return (
			<div className="container-fluid header-search " >
				<div className="container">
					<div className="row">
						<div className="col-lg-4 offset-lg-8 col-md-5 offset-md-8">
							<div className="search-part">
								<h2 className="header-text">Search Cars</h2>
								<form className="pre-owned-form" onSubmit={this.onSubmit} onReset={this.resetForm}>
									<div className="select-group">
										<MultiSelect value={this.state.make} options={make} onChange={async (e) => { await this.props.GetModelsByMakes(e.value); this.setState({ make: e.value }); }}
											className="select-item" filter={true} filterPlaceholder="Search" placeholder="Make" />
										<MultiSelect value={this.state.model} options={model} onChange={(e) => this.setState({ model: e.value })}
											className="select-item" filter={true} filterPlaceholder="Search" placeholder="Model" />
										<MultiSelect value={this.state.color} options={this.CreateOptions("color")} onChange={(e) => this.setState({ color: e.value })}
											className="select-item" filter={true} filterPlaceholder="Search" placeholder={this.props.color.name} />
										<MultiSelect value={this.state.typeOfCar} options={this.CreateOptions("Type of car")} onChange={(e) => this.setState({ typeOfCar: e.value })}
											className="select-item" filter={true} filterPlaceholder="Search" placeholder="Type of car" />
										<MultiSelect value={this.state.StateCar} options={this.CreateOptions("State")} onChange={(e) => this.setState({ StateCar: e.value })}
											className="select-item" filter={true} filterPlaceholder="Search" placeholder="State" />
										<InputNumber placeholder="Min price" value={this.state.minPrice} inputClassName="price-item" className="price-item" onChange={(e) => this.setState({ minPrice: e.value })} mode="currency" currency="USD" locale="en-US" />
										<InputNumber value={this.state.maxPrice} inputClassName="price-item" className="price-item" onChange={(e) => this.setState({ maxPrice: e.value })} mode="currency" placeholder="Max price" currency="USD" locale="en-US" />

										<button type="submit" className=" btn btn_search">Search</button>
										<button type="reset" className=" btn btn_reset">reset</button>
									</div>
									<div className="buttons-group">
										<Link to="/AdvancedSearch" className=" btn btn_buy-car"> <button type="submit" className=" btn btn_buy-car">
											<img src="../../img/car-png.png" className="btn_buy-car-image" />
                                           Advanced search
                                        </button> </Link>
									
									</div>
								</form>
							</div>
						</div>
					</div>
				</div>
			</div>

		);
	}
}
const mapStateToProps = state => {
	return {
		makes: state.filters.makes,
		models: state.filters.models,
		color: state.filters.color,
		fuel: state.filters.fuel,
		stateCar: state.filters.stateCar,
		typeOfCar: state.filters.typeOfCar,

	};
};

const mapDispatchToProps = dispatch => {
	const { GetRequestMakes, GetModelsByMakes, GetFiltersByName } = bindActionCreators(actionCreators, dispatch);
	const { GetrequestCarList } = bindActionCreators(carActions, dispatch);

	return {
		GetRequestMakes,
		GetModelsByMakes,
		GetFiltersByName,
		GetrequestCarList
	};
};
export default withRouter(connect(mapStateToProps, mapDispatchToProps)(HeaderSearch));

