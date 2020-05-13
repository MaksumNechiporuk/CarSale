import React, { Component } from 'react';
import { connect } from 'react-redux';
import { actionCreators } from "../../store/Actions/authActions";
import classnames from 'classnames';
import { bindActionCreators } from 'redux';
import { NavLink } from 'react-router-dom';


const validateemail = (email) => {
    const expression = /(?!.*\.{2})^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;

    return expression.test(String(email).toLowerCase())
}

class Registration extends Component {
    constructor(props) {
        super(props);
        this.state = {
            name: '',
            surname: '',
            email: '',
            country: '',
            city: '',
            phonenumber: '',
            password: '',
            confirmPassword: '',
            errors: {
            }
        };
    }
    changeInput(e) {
        e.preventDefault();
        let files;
        if (e.dataTransfer) {
            files = e.dataTransfer.files;
        } else if (e.target) {
            files = e.target.files;
        }
        const reader = new FileReader();
        reader.onload = () => {
            this.setState({ src: reader.result });
        };
        reader.readAsDataURL(files[0]);
    }
    setStateByErrors = (name, value) => {
        if (!!this.state.errors[name]) {
            let errors = Object.assign({}, this.state.errors);
            delete errors[name];
            this.setState(
                {
                    [name]: value,
                    errors
                }
            )
        }
        else {
            this.setState(
                { [name]: value })
        }
    }
    handleChange = (e) => {
        this.setStateByErrors(e.target.name, e.target.value);
    }
    onSubmitForm = (e) => {
        e.preventDefault();
        console.log("onSubmitForm=>", this.state);
        let errors = {};
        if (!validateemail(this.state.email)) errors.email = "Enter valid email"
        if (this.state.email === '') errors.email = "Cant't be empty"

        if (!/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{6,24}$/.test(this.state.password)) errors.password = "Password must be at least 6 characters and contain digits, upper and lower case"
        if (this.state.password === '') errors.password = "Cant't be empty"
        if (this.state.confirmPassword === '') errors.confirmPassword = "Cant't be empty"
        if (this.state.name === '') errors.name = "Cant't be empty"
        if (this.state.surname === '') errors.surname = "Cant't be empty"
        if (this.state.country === '') errors.country = "Cant't be empty"
        if (this.state.city === '') errors.city = "Cant't be empty"
        if (this.state.phonenumber === '') errors.phonenumber = "Cant't be empty"
        if (this.state.confirmPassword !== this.state.password) errors.confirmPassword = "Passwords do not match"

        const isValid = Object.keys(errors).length === 0
        if (isValid) {
            const { name, surname, email, country, city, password, phonenumber, confirmPassword } = this.state;
            this.setState({ isLoading: true });         
            let user = { name: name, surname: surname, email: email, country: country, city: city, password: password, phonenumber: phonenumber, confirmPassword: confirmPassword };
            console.log(this.props);
            this.props.Register(
                   user
            )
            let path = `/`; this.props.history.push(path);
            }
            else {
                this.setState({ errors });
            }
        
       

        }

    render() {
        const { errors, isLoading } = this.state;
        return (
            <form onSubmit={this.onSubmitForm}>
                <body>
                <main>
                <div className="loginBox">
                <h2>Create your account!</h2>
                {
                    !!errors.invalid ?
                        <div className="alert alert-dark">
                            <strong>Danger!</strong> {errors.invalid}.
                    </div> : ''}

                <div className={classnames('form-group', { 'has-error': !!errors.name })}>
                    <p>Name</p>
                    <input type="text" name="name" placeholder="Name" onChange={this.handleChange}
                        id="name"
                        name="name"
                        value={this.state.name}
                        onChange={this.handleChange} />
                                {!!errors.name ? <span className="help-block">{errors.name}</span> : ''}
                </div>

                <div className={classnames('form-group', { 'has-error': !!errors.surname })}>
                    <p>Surname</p>
                    <input type="text" name="surname" placeholder="Surname"
                        id="surname"
                        name="surname"
                        value={this.state.surname}
                        onChange={this.handleChange} />
                    {!!errors.surname ? <span className="help-block">{errors.surname}</span> : ''}
                </div>

                <div className={classnames('form-group', { 'has-error': !!errors.country })}>
                    <p>Country</p>
                    <input type="text" name="country" placeholder="Country"
                        id="country"
                        name="country"
                        value={this.state.country}
                        onChange={this.handleChange} />
                    {!!errors.country ? <span className="help-block">{errors.country}</span> : ''}
                </div>

                <div className={classnames('form-group', { 'has-error': !!errors.city })}>
                    <p>City</p>
                    <input type="text" name="city" placeholder="City"
                        id="city"
                        name="city"
                        value={this.state.city}
                        onChange={this.handleChange} />
                    {!!errors.city ? <span className="help-block">{errors.city}</span> : ''}
                </div>

                <div className={classnames('form-group', { 'has-error': !!errors.email })}>
                    <p>Email</p>
                    <input type="text" name="email" placeholder="Email"
                        id="email"
                        name="email"
                        value={this.state.email}
                        onChange={this.handleChange} />
                    {!!errors.email ? <span className="help-block">{errors.email}</span> : ''}
                </div>

                <div className={classnames('form-group', { 'has-error': !!errors.phonenumber })}>
                    <p>Phone number</p>
                    <input type="text" name="phonenumber" placeholder="Phone Number"
                        id="phonenumber"
                        name="phonenumber"
                        value={this.state.phonenumber}
                        onChange={this.handleChange} />
                    {!!errors.phonenumber ? <span className="help-block">{errors.phonenumber}</span> : ''}
                </div>

                <div className={classnames('form-group', { 'has-error': !!errors.password })}>
                    <p>Password</p>
                    <input type="password" name="password" placeholder="Password"
                        id="password"
                        name="password"
                        value={this.state.password}
                        onChange={this.handleChange} />
                    {!!errors.password ? <span className="help-block">{errors.password}</span> : ''}
                </div>

                <div className={classnames('form-group', { 'has-error': !!errors.confirmPassword })}>
                    <p>Confirm Password</p>
                    <input type="password" name="confirmPassword" placeholder="Confirm Password"
                        id="confirmPassword"
                        name="confirmPassword"
                        value={this.state.confirmPassword}
                        onChange={this.handleChange} />
                    {!!errors.confirmPassword ? <span className="help-block">{errors.confirmPassword}</span> : ''}
                </div>

                <div className="form-group">
                    <input type="submit" name="sbmt"></input>
                                <p>Already have an account? <NavLink exact href="#" to="/Login">Sign In</NavLink></p>
                                
                </div>  
                    </div>
                    </main>
                    </body>
            </form>
        );      
    }

}

const mapDispatchToProps = dispatch => {
    const { Register } = bindActionCreators(actionCreators, dispatch);
    return {
       Register
    };
};

export default connect(null,mapDispatchToProps)(Registration);

