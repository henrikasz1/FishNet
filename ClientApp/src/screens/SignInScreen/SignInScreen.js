import { View, Text, Image, StyleSheet, useWindowDimensions, ScrollView} from 'react-native';
import React, {useState} from 'react';
import Logo from '../../../assets/images/FishNetLogo.png';
import CustomInput from '../../components/CustomInput';
import CustomButton from '../../components/CustomButton';
import { useNavigation } from '@react-navigation/native';

const SignInScreen = () => {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')

  const {height} = useWindowDimensions();
  const navigation = useNavigation();

  const onSignInPressed = () => {
      console.warn("signed in");
  }

  const onForgotPasswordPressed = () => {
      console.warn("you forgot password");
  }

  const onSignUpPressed = () => {
      navigation.navigate('SignUp');
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

    <CustomButton 
        text="Sign in"
        onPress={onSignInPressed}
        type="primary"
    />

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