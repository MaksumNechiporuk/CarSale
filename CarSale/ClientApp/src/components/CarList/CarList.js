import React, { Component, Fragment } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from "../../store/Actions/CarActions";
import { Redirect, Link } from "react-router-dom";
import CarItem from "./CarItem/CarItem"
//import Paginator from 'react-library-paginator';
//import { Paginator } from 'primereact/paginator';
import { makeStyles } from '@material-ui/core/styles';
import Pagination from '@material-ui/lab/Pagination';
import ReactPaginate from 'react-paginate';
const useStyles = makeStyles((theme) => ({
	root: {
		'& > *': {
			marginTop: theme.spacing(2),
		},
	},
}));


class CarList extends Component {
	constructor(props) {
		super(props);
		this.state = {
			currentPage: 1,
			first: 1
		};
		this.handleChange = this.handleChange.bind(this);

	}

	componentDidMount() {
		this.ensureDataFetched();
		const id = this.props.match.params.id;
		this.setState({ currentPage: id });
	}
	//shouldComponentUpdate(nextState) {
	//	console.log(nextState);
	//	if (nextState.currentPage != this.state.currentPage) {
	//		return true;
	//	}
	//	else {
	//		return false;
	//	}
	//}

	async	ensureDataFetched() {
		await this.props.GetrequestCarList(this.state.currentPage, 9);
		console.log("state:", this.state);
		console.log("Props:", this.props);
	}					//<Paginator first={this.state.first} totalRecords={totalPage * 9} rows={9} onPageChange={(e) => { console.log("e", e); this.setState({ first: e.first, currentPage: e.page + 1 }); this.ensureDataFetched(); }}></Paginator>

	handleChange(event: object) {
		const selectedPage = event.selected;
		

		this.setState({
			currentPage: selectedPage + 1,
		}, () => {
			this.ensureDataFetched()
		});



	}
	render() {
		let { totalPage } = this.props.carList;

		const singleItem = this.props.carList.carList.map(item => {
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
		return (
			<Fragment>
				<div className="listCar">
					<div className="container">
						<div className="row" id="ads">
							{singleItem}
						</div>
					</div>
				</div>
				<div className="container" >

					<ReactPaginate
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