#pragma once
class Noncopyable {
private:
	Noncopyable(const Noncopyable&) {};
	Noncopyable& operator=(const Noncopyable&) {};
protected:
	Noncopyable() {};
	virtual ~Noncopyable() {};
};

