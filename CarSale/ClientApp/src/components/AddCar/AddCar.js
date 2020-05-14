import React, { Component } from "react";
import { MultiSelect } from 'primereact/multiselect';
import { InputNumber } from 'primereact/inputnumber';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from "../../store/Actions/FiltersActions"
import { actionCreators as carActions } from "../../store/Actions/CarActions";
import { withRouter } from "react-router";
import { Button } from 'primereact/button';
import { ProgressSpinner } from 'primereact/progressspinner';
import YearPicker from "react-year-picker";
import AddImage from "./AddImage/AddImage"
import uuid from "react-uuid";

class AddCar extends Component {
	constructor(props) {
		super(props);
		this.handleChange = this.handleChange.bind(this);
	}
	state = {
		make: [],
		model: [],
		filters: {},
		Price: null,
		loading: true,
		mileage: null,
		year: 0
	};
	handleChange(date) {

		this.setState(({ year }) => {
			return {

				year: date

			};

		});
	}


	componentDidMount() {
		this.ensureDataFetched();
		//if (this.props.filters)
		//	this.props.filters.map((item) => {
		//		this.setState(() => {
		//			return {

		//				[item.name]:item.

		//			};

		//		})
		//	});

	}
	async	getModels() {
		await this.props.GetModelsByMakes(this.state.make);
	}
	async	ensureDataFetched() {
		await this.props.GetRequestMakes();

		await this.props.GetAdvancedFilters();

		this.setState(({ loading }) => {
			return {

				loading: false

			};

		});
	}
	CreateOption = (propName) => {
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
	CreateOptions = (items) => {
		if (items.length != 0) {
			let options = items.map(
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
		console.log(this.state);
		let filters = [];
		this.props.filters.map((item) => {
			console.log(item.name, "=", this.state[item.name]);
			if (this.state[item.name])
				this.state[item.name].map((itemS) => {
					filters.push(itemS);
				})
		})
		if (this.state.model)
			this.state.model.map((itemS) => {
				filters.push(itemS);
			})
		console.log(filters);
		let { mileage, Price, year } = this.state;

		let new_car = {
			Date: year.toString(), UniqueName: uuid(), Name: "", Price: Price, Mileage: mileage,
			Filters: filters, MakeId:  this.state.make[0], ModelId: this.state.model[0]
		}
		console.log(new_car);
		this.props.AddNewCar(new_car);
		//let path = `/Cars/1`;
		//this.props.history.push(path);
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

		let select;
		if (this.props.filters) {
			select = this.props.filters.map(
				item => {
					return (
						<MultiSelect value={this.state[item.name]} options={this.CreateOptions(item.children)} onChange={(e) => this.setState({ [item.name]: e.value })}
							className="form-group col-sm-5 col-xs-5 select-item" filter={true} filterPlaceholder="Search" placeholder={item.name} />
					)
				});
		}
		return (
			this.state.loading ? <div className="d-flex "> <ProgressSpinner /></div> :

				<div class="container">
					<form onSubmit={this.onSubmit} className="col-sm-12 col-xs-12 AdvancedSearch"  >



						<div class="row" id="filter">

							<MultiSelect value={this.state.make} options={make} onChange={async (e) => { await this.props.GetModelsByMakes(e.value); this.setState({ make: e.value }); console.log(e.label); }}
								className="form-group col-sm-5 col-xs-5 select-item" filter={true} filterPlaceholder="Search" placeholder="Make" />
							<MultiSelect value={this.state.model} options={model} onChange={(e) => this.setState({ model: e.value, filters: { ...this.state.filters, ...e.value } })}
								className="form-group col-sm-5 col-xs-5 select-item" filter={true} filterPlaceholder="Search" placeholder="Model" />
							<InputNumber value={this.state.mileage} placeholder="Mileage" className="form-group col-sm-5 col-xs-5 select-item" onChange={(e) => this.setState({ mileage: e.value })} mode="decimal" minFractionDigits={2} />
							<InputNumber placeholder="Price" value={this.state.Price} className="form-group col-sm-5 col-xs-5 select-item" onChange={(e) => this.setState({ Price: e.value })} mode="currency" currency="USD" locale="en-US" />

							{select}
							<YearPicker className="form-group col-sm-5 col-xs-5 select-item" onChange={this.handleChange} />

						</div>
						<div className="SearchContent">
							<Button type="submit" label="Search" className="p-button-raised p-button-rounded" />

						</div>

					</form>

					<AddImage />
				</div>

		);
	}
}
const mapStateToProps = state => {
	return {
		filters: state.filters.filters,
		makes: state.filters.makes,
		models: state.filters.models,



	};
};

const mapDispatchToProps = dispatch => {
	const { GetAdvancedFilters, GetRequestMakes, GetModelsByMakes, GetFiltersByName } = bindActionCreators(actionCreators, dispatch);
	const { GetrequestCarList, AddNewCar } = bindActionCreators(carActions, dispatch);

	return {
		GetAdvancedFilters,
		GetrequestCarList,
		GetRequestMakes,
		GetModelsByMakes,
		GetFiltersByName,
		AddNewCar

	};
};
export default withRouter(connect(mapStateToProps, mapDispatchToProps)(AddCar));
