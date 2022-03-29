import { View, Text, Image, StyleSheet, useWindowDimensions, ScrollView, ActivityIndicator} from 'react-native';
import React, {useState} from 'react';
import Logo from '../../../assets/images/FishNetLogo.png';
import CustomInput from '../../components/CustomInput';
import CustomButton from '../../components/CustomButton';
import CustomMsgBox from '../../components/CustomMessageBox';
import { useNavigation } from '@react-navigation/native';
import { BaseUrl } from '../../components/Common/BaseUrl'
import axios from 'axios';

const SignInScreen = () => {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [message, setMessage] = useState('')
  const [isSubmitting, setIsSubmitting] = useState(false)

  const {height} = useWindowDimensions();
  const navigation = useNavigation();

  const onSignInPressed = () => {
      setIsSubmitting(true)
      handleMessage('')
      if (email == '' || password == '') {
          handleMessage('Please fill in all fields');
          setIsSubmitting(false);
      }
      else {
          handleLogin({email, password});
      }
  }

  const onForgotPasswordPressed = () => {
      console.warn("you forgot password");
  }

  const onSignUpPressed = () => {
      navigation.navigate('SignUp');
  }

  const handleLogin = (credentials) => {
    const url = `${BaseUrl}/api/user/login`;

    axios
        .post(url, credentials)
        .then((response) => {
            const result = response.data;
            const {token, success, errors} = result;
            axios.defaults.headers.common['Authorization'] = 'Bearer ' + token;
            navigation.navigate('MainScreen');
        })
        .catch(error => {
            setIsSubmitting(false)
            handleMessage("Invalid email or password")
        })
  }

  const handleMessage = (message) => {
      setMessage(message);
  }

  return (
    <ScrollView showsVerticalScrollIndicator={false}>
    <View style={styles.root}>
    <Image source={Logo} style={[styles.logo, {height: height * 0.4}]} resizeMode="contain" />
    
    <CustomInput
        placeholder="Email address"
        iconType="user"
        value={email}
        setValue={setEmail}
    />

    <CustomInput
        placeholder="Password"
        iconType="lock"
        value={password}
        setValue={setPassword}
        secureTextEntry={true}
    />

    <CustomMsgBox text={message} />

    {!isSubmitting ?
        <CustomButton 
            text="Sign in"
            onPress={onSignInPressed}
            type="primary"
        />
        :
        <CustomButton
            text={<ActivityIndicator size="small" color="white" />}>
        </CustomButton>
    }

    <CustomButton
        text="Sign up"
        onPress={onSignUpPressed}
        bgColor="#E7EAF4"
        fColor="#4765A9"
    />

    <CustomButton
        text="Forgot Password?"
        onPress={onForgotPasswordPressed}
        type="tertiary"
    />

    </View>
    </ScrollView>
  )
}

const styles = StyleSheet.create({
    root: {
        alignItems: 'center',
        padding: 20,
    },
    logo: {
        width: '70%',
        maxWidth: 200,
        maxHeight: 200,
    },
});

export default SignInScreen