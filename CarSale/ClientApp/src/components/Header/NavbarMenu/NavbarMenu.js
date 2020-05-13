import React, { Component } from 'react';
import { NavLink } from 'react-router-dom';
class NavbarMenu extends Component {

	render() {
		return (
			<div className="navbar-menu hidden">
				<div className="container">
					<div className="col">
						<div className="row">
							<ul className="navbar-pc d-none d-lg-block">
								<li className="home-icon"><i className="fa fa-home fa-2x"></i></li>
								<li><NavLink activeClassName="nav_active" exact href="#" to="/" className="nav-item">Home</NavLink></li>
								<li><NavLink activeClassName="nav_active" href="#" to="/Cars/1" className="nav-item">Cars</NavLink></li>
								<li><a href="#" className="nav-item">Contact Us</a></li>
								<li><a href="#" className="nav-item">About Us</a></li>
								<li><NavLink activeClassName="nav_active" href="#" to="/AdminPanel/filters" className="nav-item">Admin Panel</NavLink></li>
								<button type="submit" className=" btn btn_account">
									<i className="fa fa-user"></i> My Account
                                    <i className="fa fa-chevron-down"></i>
								</button>
								<button type="submit" className="btn btn_register">Register</button>
								<button type="submit" className="btn btn_login">Login</button>
							</ul>
							<div
								className="phone-burger d-none d-block d-sm-none d-sm-block d-xs d-lg-none"
							>
								<div className="wrapper">
									<input type="checkbox" id="check-menu" />
									<label for="check-menu">MENU</label>
									<div className="burger-line first"></div>
									<div className="burger-line second"></div>
									<div className="burger-line third"></div>
									<div className="burger-line fourth"></div>
									<nav className="main-menu">
										<a href="#">Home</a>
										<a href="#">Cars</a>
										<a href="#">Contact Us</a>
										<a href="#">About Us</a>
										<a href="#">Admin Panel</a>
										<a href="#">Login</a>
										<a href="#">Register</a>
									</nav>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>

		);
	}
}

export default NavbarMenu;