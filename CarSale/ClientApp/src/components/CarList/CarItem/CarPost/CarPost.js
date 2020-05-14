import React, { Component, Fragment } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from "../../../../store/Actions/CarActions";
import { Lightbox } from 'primereact/lightbox';
import { TabView, TabPanel } from 'primereact/tabview';
import { ProgressSpinner } from 'primereact/progressspinner';
import AwesomeSlider from 'react-awesome-slider';
import 'react-awesome-slider/dist/styles.css';
import ShowProfile from "../../../Profile/ShowProfile/ShowProfile"

class CarPost extends Component {
	constructor() {
		super();
		this.state = {
			images: null,
			mainImage: "",
			loading: true
		};
	}
	componentDidMount() {
		this.ensureDataFetched();
	}
	async	ensureDataFetched() {
		const id = this.props.match.params.id;
		await this.props.GetCarById(id);
		await this.props.OwnerByCarId(id);
		this.setState(({ mainImage, loading }) => {
			return {

				mainImage: this.props.selectCar.image[0],
				loading: false

			};

		});
	}
	render() {
		let { name, price } = this.props.selectCar;
		let { CarsOwner } = this.props;
		let { loading } = this.state;
		var formatter = new Intl.NumberFormat('en-US', {
			style: 'currency',
			currency: 'USD',
		});
		price = formatter.format(price);
		let features, images, ImageForGallery;
		if (!loading) {
			features = this.props.selectCar.filters.map(item => {
				return (<li key={item.name}>{item.name}: {item.children.name}</li>)
			});
			images = this.props.selectCar.image.map(item => {
				return (<div className="image" key={item} data-src={item} />)
			});
			ImageForGallery = this.props.selectCar.image.map(item => {
				return ({
					source: `${item}`,
					thumbnail: `${item}`,
					title: 'Car image'
				})
			});
			console.log(CarsOwner);
		}
		return (

			loading ? <div className="d-flex "> <ProgressSpinner /></div> :

				<Fragment>
					<div className="container carPost">

						<div className="row">
							<div className="information_block">
								<div className="col-md-7">
									<AwesomeSlider bullets={false} fullScreen={true} className="image" showPlayButton={false}>
										{images}
									</AwesomeSlider>
								</div>
								<div className="col-md-5 informationCar">
									<h2 className="carName">{name}</h2>
									<p className="product-price">{price}</p>

								</div>
							</div>


						</div>
						<TabView>
							<TabPanel header="DESCRIPTION">
								<h3>{name} Features:</h3>
								<ul>
									{features}
								</ul>
							</TabPanel>
							<TabPanel header="Photo">
								<Lightbox className="imgGallery" type="images" images={ImageForGallery} />


							</TabPanel>
							<TabPanel header="Car owner">
								<div className="container">
									<div className="row">
										<div className="col-sm-2 col-md-2">
											<img src={CarsOwner.img}
												alt="" className="img-rounded img-responsive" />
										</div>
										<div className="col-sm-4 col-md-4">
											<blockquote>
												<p>{CarsOwner.name}</p> <small><cite title="Source Title">{CarsOwner.city}, {CarsOwner.country}  <i className="fa fa-map-marker" aria-hidden="true"></i></cite></small>
											</blockquote>
											<p> <i className="fa fa-envelope-o"></i> {CarsOwner.email}

												<br
												/> <i className="fa fa-phone" aria-hidden="true"></i> {CarsOwner.phone}

												<br />
											</p>

										</div>
									</div>
								</div>

							</TabPanel>
						</TabView>
					</div>
				</Fragment>
		);
	}
}
const mapStateToProps = state => {

	return {
		selectCar: state.carList.selectCar,
		CarsOwner: state.carList.CarsOwner
	};
};

const mapDispatchToProps = dispatch => {
	const { GetCarById, OwnerByCarId } = bindActionCreators(actionCreators, dispatch);
	return {
		GetCarById,
		OwnerByCarId
	};
};

export default connect(mapStateToProps, mapDispatchToProps)(CarPost);