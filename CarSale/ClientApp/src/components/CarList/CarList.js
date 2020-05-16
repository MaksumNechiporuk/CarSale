import React, { Component, Fragment } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from "../../store/Actions/CarActions";
import { Link } from "react-router-dom";
import CarItem from "./CarItem/CarItem"
import ReactPaginate from 'react-paginate';
import { ProgressSpinner } from 'primereact/progressspinner';
import { Redirect } from "react-router-dom";
import NotFound from "../NotFound/NotFound"
class CarList extends Component {
	constructor(props) {
		super(props);
		this.state = {
			currentPage: this.props.match.params.page,
			first: 1,
			loading: true
		};
		this.handleChange = this.handleChange.bind(this);
		this.myRef = React.createRef()
	}
	componentDidMount() {

		this.ensureDataFetched();

	}
	async	ensureDataFetched() {
		console.log("filters", this.props);
		await this.props.GetrequestCarList(this.state.currentPage, 9, this.props.selectFilters, this.props.selectMake, this.props.maxPrice, this.props.minPrice);
		this.setState({
			loading: false
		});
	}
	handleChange(event: object) {
		const selectedPage = event.selected;
		console.log(selectedPage);
		this.setState({
			currentPage: selectedPage + 1,
			loading: true
		}, async () => {
			await this.ensureDataFetched()
			this.setState({
				loading: false
			});
		});
		window.scrollTo(0, this.myRef.current.offsetTop)
		let path = `/Cars/${selectedPage + 1}`;
		this.props.history.push(path);
	}
	RenderRedirect() {
		return <Redirect to="/" />;

	}
	render() {
		let { totalPage } = this.props.carList;
		let { loading, currentPage } = this.state;
		let singleItem = null;
		if (this.props.carList.carList) {
			singleItem = this.props.carList.carList.map(item => {
				let path = "/CarPost/" + item.id;
				return (
					<Link to={path} className="unlinkCar col-xl-4 col-sm-12 col-md-6">
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
					</Link>
				);
			});
		}
		//console.log("this.props.carList.carList.lenght == 0", this.props.carList.carList.length);
		if (this.props.carList.carList)
			if (this.props.carList.carList.length == 0)
				console.log("this.props.carList.carList.lenght == 0");
		return (
			this.props.carList.carList.length == 0 ? <h2 className="NotFoundCar">Car Could Not Be Found!</h2> :

				loading ? <div className="d-flex "> <ProgressSpinner /></div> :

					<Fragment>
						<div className="listCar">
							<div className="container">

								<div className="row" ref={this.myRef} id="ads">
									{singleItem}
								</div>
							</div>
						</div>
						<div className="container" >
							<ReactPaginate
								forcePage={this.props.match.params.page - 1}
								previousLabel={"<<"}
								nextLabel={">>"}
								breakLabel={"..."}
								breakClassName={"break-me"}
								pageCount={totalPage}
								marginPagesDisplayed={3}
								pageRangeDisplayed={3}
								onPageChange={this.handleChange}
								containerClassName={"pagination "}
								subContainerClassName={"pages"}
								activeClassName={"current"} />
						</div>
					</Fragment >
		);
	}
}
const mapStateToProps = state => {
	return {
		carList: state.carList,
		totalPage: state.totalPage,
		selectFilters: state.carList.selectFilters,
		selectMake: state.carList.selectMake,
		maxPrice: state.carList.maxPrice,
		minPrice: state.carList.minPrice
	};
};

const mapDispatchToProps = dispatch => {
	const { GetrequestCarList } = bindActionCreators(actionCreators, dispatch);
	return {
		GetrequestCarList
	};
};

export default connect(mapStateToProps, mapDispatchToProps)(CarList);