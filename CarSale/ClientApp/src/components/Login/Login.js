import React, { Component } from 'react';
import { connect } from 'react-redux';
import { actionCreators } from "../../store/Actions/authActions";
import { bindActionCreators } from 'redux';
import classnames from 'classnames';

const validateemail = (email) => {
    const expression = /(?!.*\.{2})^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;

    return expression.test(String(email).toLowerCase())
}

class Login extends Component {
    constructor(props) {
        super(props);
        this.state = {  
            email: '',
            password: '',
            errors: {
            },
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

        const isValid = Object.keys(errors).length === 0
        if (isValid) {
            const {email,password} = this.state;
            this.setState({ isLoading: true });


            let user = {email: email, password: password};
            console.log(this.props);
            this.props.Log(
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
                            <h2>Sign in!</h2>
                            {
                                !!errors.invalid ?
                                    <div className="alert alert-danger">
                                        <strong>Danger!</strong> {errors.invalid}.
                    </div> : ''}                    
                            <div className={classnames('form-group', { 'has-error': !!errors.email })}>
                                <p>Email</p>
                                <input type="text" name="email" placeholder="Email"
                                    id="email"
                                    name="email"
                                    value={this.state.email}
                                    onChange={this.handleChange} />
                                {!!errors.email ? <span className="help-block">{errors.email}</span> : ''}
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
                            <div className="form-group">
                                <input type="submit" name="sbmt"></input>                               
                            </div>
                        </div>
                    </main>
                </body>
            </form>
        );
    }
}

const mapDispatchToProps = dispatch => {
    const { Log } = bindActionCreators(actionCreators, dispatch);
    return {
        Log
    };
};

export default connect(null, mapDispatchToProps)(Login);

